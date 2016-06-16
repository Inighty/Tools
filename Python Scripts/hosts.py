# -*- coding: utf-8 -*-

import sys,urllib,os
import urllib.request

url = "https://raw.githubusercontent.com/racaljk/hosts/master/hosts" #网页地址

def Get517Str(content,startStr):
  if content.find("#517na start") != -1:
    startIndex = content.index(startStr)
    return content[startIndex:len(content)]
  else:
    return ''


wp = urllib.request.urlopen(url) #打开连接

newhostsdata = wp.read() #获取页面内容

oldhostsdata = open(os.getenv("SystemDrive") + '\\Windows\\System32\\drivers\\etc\\hosts').read()
needstaystr = Get517Str(oldhostsdata,"#517na start")

newhosts = newhostsdata.decode('ascii') + needstaystr if (len(needstaystr)==0) else ("\n"+needstaystr)

fp = open(os.getenv("SystemDrive") + '\\Windows\\System32\\drivers\\etc\\hosts','w')
fp.write(newhosts)
fp.close( )

dnscmd = 'ipconfig/flushdns'
os.system(dnscmd)