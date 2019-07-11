> 本项目 forked from [kavilee2012/Warehouse](https://github.com/kavilee2012/Warehouse)，在 ADO.NET 开发技术课程中被作为实验课的授课案例接触到本项目，最终抱着学习的目的 forked
>
> 相比于原项目，我做了以下工作：
>
> - 整理了项目结构，使得更清晰
> - 解决原项目存在的 Bug，原项目存在的 Bug 可以在文件 [DebugList.md](./DebugList.md) 中查看
> - 在代码中添加了十足的注释，便于理解项目（毕竟是学习项目）
> - 根据原项目逆推出 [需求文档](./Documents/需求分析.md) 和 [使用文档](./Documents/使用文档.doc) ，便于理解项目（毕竟是学习项目）
> - 其他一些小修改



# 简单条码库存管理系统

## 业务流程

货物入仓 => 填写入仓记录(分配条码) => 打印条码 => 给每件货物粘贴条码 => 填写供货单 => 打印供货单 => 货物出仓

## 系统功能

1. 货物
    1. 入库
        1. 入库记录显示/添加/删除
        2. 货物条形码打印
    2. 出库
        1. 出库记录显示/添加/删除
        2. 供货单显示
    3. 查看
        1. 库存查看(剩余数量)
        2. 统计查询(入、出库数量)
2. 系统用户
    1. 用户显示/添加/检索/更新
    2. 软件锁定

## 系统目标

通过条码管理库存，提高成品出入库管理的工作效率、准确性、实时性。当有退换货时可迅速查找出问题货源。可实时生成各种统计报表。

## 系统特点

- 可能实现了 TSC 条码打印机打印条码（无设备无法测试）

- 暂未实现扫描枪扫描条码出仓（亦无设备~）

## 开发环境

技术：Winform（C#）

工具：Visual Studio 2008 + SSMS

数据库：SqlServer 2017 Dev

## 项目使用

根据文件 [DatabaseSetting/readme.md](./DatabaseSetting/readme.md) 的提示配置数据库环境

在 Warehouse 项目的配置文件中添加数据库连接字符串即可使用

> 项目在本机编译测试无误，但无法保证任何主机都能如此。如果在你的系统上出现编译错误，还请根据编译提示自行修复

建议阅读一下 [需求文档](./Documents/需求分析.md) 和 [使用文档](./Documents/使用文档.doc) 后再开始使用本项目