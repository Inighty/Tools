::由于windows任务计划不支持直接运行python脚本，编写此bat来间接执行
@echo off 
C: 
cd C:\Python27\test 
start pythonw AcfunSignAndZhihu.py 
exit 