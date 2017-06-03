# ZigBee定位系统
# 华中科技大学管理学院大数据实验室 Archieved by xinze.zh

该目录主要包括ZigBee的源代码src、发布release及文档。

//注意：使用前解压ZigBee-PositionManage_config文件夹，并运行其中的配置程序。注意：同一时间只能一台主机与中继器建立连接。

//ZigBee-PositionManage_config/USR_TCP232-Setup V4.22

//参数设置区
//模块工作方式：UDP Mode（请勿更改；若更改，请重写当前主机与中继器的连接函数）
//本模块IP地址：192.168.0.197（请勿更改；该IP地址为中继器IP地址）
//子网掩码：255.255.255.0（请勿修改；默认）
//模块默认网关：192.168.0.254（请勿修改；默认）
//串口波特率：115200（请勿修改；标准波特率为115200）
//校验/数据/停止：NONE；8；1（请勿修改）
//模块自身端口：20108
//连接目标IP：192.168.0.***（设置为将要连接ZigBee中继器的主机IP）
//连接目标端口：8567（请勿更改；默认端口为8567）


//为避免src中exe应用程序的release提示，已将src中 ./bin 和 ./obj 两个文件夹压缩。请同学们clone之后进行解压。

#The project includes source code, release,doc, etc...

#Due to commercial confidentiality, some of the SRC and MFC is not opensource.
