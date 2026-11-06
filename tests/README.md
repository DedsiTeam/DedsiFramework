# DedsiFramework 测试

测试针对框架自身的行为，不要求外部 SQL Server、MySQL、RabbitMQ，也不访问真实业务数据。
使用 .NET 10、xUnit、Moq 和 Coverlet。测试项目不打包为 NuGet。

## 运行

在仓库根目录执行：

```sh
dotnet test Dedsi.Framework.slnx -c Release
```

只运行单元测试（不启动 ABP 集成夹具）：

```sh
dotnet test Dedsi.Framework.slnx --filter 'Category!=Integration'
```

运行 SQLite 和模块注册集成测试：

```sh
dotnet test Dedsi.Framework.slnx --filter 'Category=Integration'
```

生成测试结果及覆盖率：

```sh
dotnet test Dedsi.Framework.slnx -c Release \
  --logger 'trx;LogFileName=tests.trx' \
  --collect:'XPlat Code Coverage' \
  --settings tests/coverage.runsettings \
  --results-directory TestResults
```

报告保存在 `TestResults`，已由现有 `.gitignore` 排除。覆盖率仅统计 Dedsi 生产程序集，排除测试程序集和显式生成代码；保留异步方法的状态机覆盖率。覆盖率不能代替业务正确性证明。

## 覆盖范围

| 范围 | 验证内容 |
| --- | --- |
| Core | JSON/XML、UTF-8、AES 往返、MD5 已知向量、HTML 文本提取、表达式组合、DataTable、枚举、日期、Claims 和日志 |
| Application.Contracts | 分页计算与默认修正、JSON 导出标记隔离、长整型总数 |
| 应用服务事务 | 经真实 ABP 代理调用，验证自动提交、已刷入数据的异常回滚、加入外层工作单元且不提前提交 |
| EntityFrameworkCore | 创建审计、保留外部赋值、私有 setter、兼容 ABP 审计；SQLite 分页、筛选、排序、无跟踪、软删除过滤、SQL 参数化、取消 |
| AspNetCore | Excel/PDF 文件响应、异常 JSON 结构、业务错误码、友好消息 |
| SqlSugar | SQL Server/MySQL 配置、每个作用域独立的客户端，不打开数据库连接 |
| 模块组合 | 加载所有 Dedsi 模块后的自定义服务注册；Domain / Infrastructure 单独加载不引入 Application 层，显式组合仍可加载 Application |

SQLite 测试每例创建独立应用、连接和数据库；工作单元在测试方法的异步上下文内建立。测试模块关闭自身程序集的自动服务扫描，避免测试替身被注册进生产服务接口。

CAP、FastEndpoints 和部分架构模块目前只有依赖声明，所以验证模块组合注册，不假装已覆盖消息投递或真实 HTTP 端点。SQL Server/MySQL 方言、CAP 事务消息、完整 HTTP 宿主的授权以及跨数据库、消息队列事务需要在后续业务集成测试中验证。单元测试不扫描依赖漏洞，也不替代发布前安全审查。

## 架构依赖约定

`Dedsi.CleanArchitecture.Domain` 只依赖 `Dedsi.Ddd.Domain`，不再隐式引入 Application 或 Application.Contracts。`Dedsi.CleanArchitecture.Infrastructure` 通过 Domain 和 EF Core 组合基础设施，也不隐式加载应用层。

需要应用服务的业务项目，应显式引用 `Dedsi.Ddd.Application` 并在业务应用模块中声明对 `DedsiDddApplicationModule` 和自身 Domain 模块的依赖。测试项目也显式引用 Application，以验证完整模块组合。原先依靠 CleanArchitecture.Domain 传递获得应用层类型或模块初始化的项目，需要按此方式调整。

### 模块接入和事务边界

模块引用与 ABP 依赖声明保持对应，单独依赖入口模块即可加载其所需模块：

| 入口模块 | 补齐的直接依赖 |
| --- | --- |
| DedsiDddDomainModule | DedsiCoreModule、DedsiDddDomainSharedModule |
| DedsiDddApplicationContractsModule | DedsiCoreModule、DedsiDddDomainSharedModule |
| DedsiEntityFrameworkCoreModule | DedsiDddDomainModule |
| DedsiAspNetCoreModule | DedsiDddApplicationContractsModule |

13 个生产模块均有独立加载与依赖顺序测试，保留领域层不引入应用层的边界测试。已移除的命令分发模块不再有注册或事务接入要求。

事务继续使用 ABP 原生工作单元：

- 业务用例继承 `DedsiApplicationService`，从支持 ABP 拦截器的 DI 容器解析后调用。类代理入口应为 public virtual 异步方法；直接 `new` 服务、类内部自调用不能作为可靠的事务拦截入口。
- 默认写操作自动建立事务；成功后完成工作单元，异常时回滚。已有工作单元时默认加入它，不在嵌套应用服务返回时提前提交。`autoSave: true` 表示刷入数据库，不表示外层事务已提交。
- 后台任务或普通服务需要跨多次仓储调用保持原子性时，应调用受拦截的应用服务，或显式使用 `IUnitOfWorkManager.Begin(isTransactional: true)` 并在成功后 `CompleteAsync()`。不要假定多个独立仓储调用天然属于同一个事务。
- 这些保证受 ABP 的事务选项、方法属性和宿主代理配置控制。测试使用 Autofac 与内存 SQLite，直接从容器调用真实的应用服务，不手工给被测入口包裹事务；另外验证其加入外层事务后随外层回滚。
- 当前 SqlSugar 客户端注册没有实现 ABP 工作单元事务桥接，CAP 也只是依赖集成；上述 EF Core 验证不代表 SqlSugar、CAP 已自动参与同一个事务。

## 基础工具契约

- 默认 JSON 输出 camelCase，默认反序列化忽略属性名大小写，支持原始 PascalCase 与 camelCase 往返。自定义命名策略可通过带 `JsonSerializerOptions` 的序列化和反序列化重载配对使用；传入的 Encoder 和其他策略不会被覆盖，支持只读配置与并发复用。
- 分页 DTO 默认第 1 页、每页 10 条。页码小于 1 回退为 1，页大小不在 1～1000 时回退为 10；赋值时即生效，`GetSkipCount()` 不修改属性。DTO 通过 `IValidatableObject` 接入 ABP / ASP.NET Core 参数验证，偏移量超出 int 范围时返回验证错误（400）。直接调用 `GetSkipCount()` 或仓储仍抛出 `ArgumentOutOfRangeException`；仓储直接调用还会拒绝非法页码和页大小。其他内部参数异常不会被全局映射为 400。
- 枚举描述：存在命名字段和 Description 时返回描述；无描述、未定义值、未命名 Flags 组合返回空字符串。null 参数抛出 `ArgumentNullException`。
- 审查 #9、#10、#12、#14 的回归用例已转为常规测试；#13 的重名扩展已随相关项目移除。

## 安全与接口错误契约

- `DedsiGlobalExceptionHandler` 保留 `error.message` / `error.code` 结构，从请求作用域解析 ABP 的 `IHttpExceptionStatusCodeFinder` 和 `IExceptionToErrorInfoConverter`。即使宿主开启详细错误，本处理器仍禁用详情和堆栈输出，响应不包含 Details 或 Data；完整异常保留在服务端日志。
- 用户友好异常保留可展示消息；普通业务异常通过 ABP 本地化映射生成消息，未配置映射时不直接返回其原始 Message。状态码遵循 ABP：验证错误 400、未登录授权失败 401、已登录授权失败 403、实体不存在 404、并发冲突 409、未知内部错误 500。业务错误默认 403，可通过 `AbpExceptionHttpStatusCodeOptions.Map` 覆盖。
- 本地异常有业务错误码时保留原码；没有时使用 `global-error-{实际状态码}`。这意味着无业务码的友好异常不再返回 `global-error-500`，而是 `global-error-403`。
- 已开始的响应、客户端取消请求不在本处理器中重写，由宿主处理。该处理器仍由宿主按原有方式接入异常处理中间件。
- 单一叶子异常的 `AggregateException` 展开后，状态码与消息使用同一异常转换；日志仍记录原始包装异常。包含多个异常时使用安全兜底消息，不随意展示第一个业务异常。
- `AbpRemoteCallException` 默认不透传上游 Message、Details、Data 或未经允许的错误码。状态码仍委托 ABP 的状态码查找器；需要展示远程业务提示时，按错误码配置本地安全消息：

```csharp
// 业务模块 ConfigureServices 中；命名空间 Dedsi.AspNetCore.Middlewares
Configure<DedsiExceptionHandlingOptions>(options =>
{
    options.RemoteErrorMessages["ORDER:Unavailable"] = "库存不足";
});
```

此配置仅影响 `DedsiGlobalExceptionHandler`，不替换 ABP 原生异常处理器。
- `DedsiPagedRequestDto.IsExport` 改为公开读取、私有赋值，并保留 `[JsonIgnore]`。Query / Form / JSON 均不能开启它。原来的 `request.IsExport = true` 需要改成 `request.EnableExport()`，仅在服务端确认导出权限后调用。
- CAP 和 SqlSugar 显式引用 `System.Security.Cryptography.Pkcs`，版本跟随 `MicrosoftVersion`（当前 10.0.14），覆盖 SqlClient / Oracle 传递引入的旧版。这些引用会写入生产 NuGet 包的依赖列表，不能设置为 `PrivateAssets=all`。修复依据：[PKCS 安全公告](https://github.com/advisories/GHSA-846f-f764-76mx)。
- CI 在还原时审计直接和传递依赖，将 NU1901～NU1904 作为错误阻止发布。手动核对可运行 `dotnet list Dedsi.Framework.slnx package --include-transitive --vulnerable`；审计结果受所配置包源和当时公开公告影响。
- 审查 #2、#3、#11 的测试已转为常规用例，并补充授权、并发、业务码映射、已开始响应、取消、大小写字段和 Form 绑定场景。

## 删除与创建审计契约

### 普通删除和直接物理删除

- `DeleteManyAsync(predicate, autoSave, cancellationToken)` 查询匹配实体，再委托 ABP 的实体批量删除流程。支持软删除、删除审计、实体事件；返回匹配实体数量。`autoSave=false` 将保存留给工作单元，`autoSave=true` 会 SaveChanges，但不单独提交外层事务。
- `DeleteDirectWithCountAsync(predicate, cancellationToken)` 直接执行物理删除 SQL 并返回受影响行数。保留当前查询过滤器；不填充删除审计、不发实体删除事件、不更新 ChangeTracker，也不会主动 SaveChanges 保存其他待处理实体。它会加入当前数据库事务；外层事务回滚时删除也回滚。调用方不要继续使用此前跟踪的已删除实体。
- ABP 原有不返回行数的 `DeleteDirectAsync` 仍可使用。
- 迁移：如果旧代码刻意依赖 `DeleteManyAsync` 的物理删除行为，请改为 `DeleteDirectWithCountAsync`，移除 `autoSave` 参数。普通删除会加载匹配实体，不能把它当成无实体加载的大数据清理接口。自定义 `IDedsiRepository` 实现需要实现新增方法。
- SQLite 测试覆盖软删除、删除时间与操作者、事件、延迟保存、物理删除、过滤器、无匹配、取消，以及两种删除随外层事务回滚。直接删除测试还验证不会把其他已跟踪实体的修改提前写入数据库。

### 明确创建人，姓名可回退

- 实现 `IDedsiHasCreationId` 的实体必须有非空 Guid 创建人：优先保留显式 CreatorId，否则从当前用户获得。匿名且未指定创建人、或者当前用户 ID 为 Guid.Empty 时拒绝创建。
- 缺少 CreatorName 时，当前身份与创建人一致则依次使用显示姓名、用户名、创建人 ID 字符串。显式指定了不同创建人时只使用其 ID 回退，不错误套用当前操作者姓名。
- 已指定的姓名、ID 和创建时间保持不变；缺少创建时间时使用 IClock。验证先于赋值，避免缺少身份时留下部分填充字段。标准 ABP 审计仍委托父类处理。
- `DedsiAuditOptions.CheckCreatorTenant` 默认 `false`，保持当前项目的跨租户审计填充行为。开启后，用户租户与 `IMultiTenant.TenantId` 不一致且未显式指定 Dedsi 创建人时，在赋值前拒绝操作；显式创建人仍保留，但缺失姓名只回退到其 ID，不借用其他租户的当前用户姓名。标准 ABP 实体仍交给父类处理。
- 需要启用严格检查的项目可在业务模块中配置（命名空间 `Dedsi.EntityFrameworkCore.PropertySetters`）：

```csharp
Configure<DedsiAuditOptions>(options => options.CheckCreatorTenant = true);
```

- 后台任务应在业务层提供显式创建人字段，或用 ABP 的 `ICurrentPrincipalAccessor.Change(...)` 在受控作用域内设置稳定的系统用户身份。不要每次随机生成系统 ID，也不默认将 Guid.Empty 当成系统账号。身份切换作用域必须覆盖实际保存，保存完成后释放。
- 系统身份方式示意（`systemUserId` 应由业务配置提供）：

```csharp
using (currentPrincipalAccessor.Change(new ClaimsPrincipal(new ClaimsIdentity(
    new[]
    {
        new Claim(AbpClaimTypes.UserId, systemUserId.ToString()),
        new Claim(AbpClaimTypes.UserName, "scheduled-import")
    }, "SystemJob"))))
{
    await applicationService.ImportAsync();
}
```

上述类型分别来自 `System.Security.Claims` 和 `Volo.Abp.Security.Claims`。若在外部工作单元中延迟保存，需要保持身份作用域直到 `CompleteAsync()` 完成。

原先跳过的匿名创建测试已按最终策略改为“没有身份必须拒绝”，同时增加显式系统创建人和姓名回退测试。所有待修复测试已转为普通测试，删除了 KnownIssue 跳过机制；这不表示尚未接入的外部数据库和消息队列场景已获得验证。

## CI

现有发布工作流依次执行整个解决方案的还原、Release 构建、测试，再打包发布。测试失败会阻止后续打包发布；无论成功或失败均尝试上传 TRX 和 Cobertura 报告。只打包 `src` 下的生产项目。

发布遇到已存在的包版本会失败，不再用 `--skip-duplicate` 静默跳过。版本由 `Directory.Build.props` 统一维护；本轮保留已有的 10.7.1，不执行实际发布。

本轮兼容性以调整前的当前代码为基准：CQRS 继续移除，保留现有公开方法、审计构造函数、分页修正规则和错误响应结构。前几轮涉及 IsExport、领域依赖及删除语义的迁移说明仍适用于尚未完成迁移的更早项目。

当前测试集无已知问题跳过用例；CI 会执行全部已接入的回归测试。
