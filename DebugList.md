# Debug List

本项目 forked 自 [kavilee2012/Warehouse](https://github.com/kavilee2012/Warehouse)，原项目存在一点小问题，问题与解决如下：

> 本 Repository 的 Warehouse_Desktop 已修复下列问题

1. 菜单栏“成品入仓”界面的“入仓”提示“入仓失败”
    原因：数据库中表 “InW” 缺少列"Model(varchar(50))", "Machine(int)", "Length(int)"；表“InWDetail”缺少列"PrintCnt(int)", "Length(int)", "Model(varchar(50))"

2. “系统管理-公司信息设置”异常
    原因：数据库中缺少表 Argument(从项目 Common 的 Common Service 的函数 GetParamValue() 和 SetParamValue() 可以看出操作的这个表)，创建表 `Argument( ArgName(varchar(50)), ArgValue(varchar(150)) )`，添加数据 `(Name, null), (Phone, null), (Address, null), (GoodsName, null) `即可

3. “成品出仓-生成供货单”失败
    原因：数据库中表 SupplyDetails 缺少列 "Length(int)", "Model(varchar(50))" 

4. 打开了 frmSupplyReport 窗体之后关闭程序弹出警告““System.CannotUnloadAppDomainException”类型的未经处理的异常出现在 mscorlib.dll 中。其他信息: 卸载 Appdomain 时出错。 (异常来自 HRESULT:0x80131015)”，

    - 原因：MS ReportViewer 控件的已知 Bug(https://stackoverflow.com/questions/9061808/how-to-handle-a-system-cannotunloadappdomainexception)
    - 解决：在窗体中的 `formClosing` 事件中添加如下代码：

    	``` C#
    	reportViewer1.LocalReport.ReleaseSandboxAppDomain();
    	```
    - 其他：ReportViewer 不是自带的控件，需要安装，下载地址:https://www.microsoft.com/zh-cn/download/details.aspx?id=6576