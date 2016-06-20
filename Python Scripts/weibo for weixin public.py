#-*-coding:utf8-*-
#不用微博，但是想知道老胡和JJ的最新微博。。
import cookielib
import json
import re
import urllib
import urllib2
import time
import sys
import bs4
import requests
import setproctitle
setproctitle.setproctitle('weibo')
reload(sys)
sys.setdefaultencoding('utf-8')

#登陆微信公众平台
def LoginPost(allAnswer):
    cj=cookielib.LWPCookieJar()
    opener = urllib2.build_opener(urllib2.HTTPCookieProcessor(cj))
    urllib2.install_opener(opener)
    #ziji
    myfakeid = 'oI8CEs164CfsmGRomlj5NPHDEdX8'
    #ttt
    tttfakeid = 'oI8CEsymDyTNUv0TYj1EBp7PA7kI'
    #登陆
    paras = {'username':'username','pwd':md5("pwd"),'imgcode':'','f':'json'}
    req = urllib2.Request('https://mp.weixin.qq.com/cgi-bin/login?lang=zh_CN',urllib.urlencode(paras))
    req.add_header('Accept','*/*')
#    req.add_header('Accept-Encoding','gzip, deflate')
    req.add_header('Accept-Language','zh-CN,zh;q=0.8')
    req.add_header('Connection','keep-alive')
    req.add_header('Content-Length','81')
    req.add_header('Content-Type','application/x-www-form-urlencoded; charset=UTF-8')
    req.add_header('Host','mp.weixin.qq.com')
    req.add_header('Origin','https://mp.weixin.qq.com')
    req.add_header('Referer','https://mp.weixin.qq.com/')
    req.add_header('User-Agent','Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.75 Safari/537.36')

    ret = urllib2.urlopen(req)
    retread = ret.read()
    print  retread
    retcontent = json.loads(retread)
    ErrMsg = retcontent['base_resp']['err_msg']
    print  "Login status: ",ErrMsg
    RedirectUrl = retcontent['redirect_url']
    print "redirect url: ",RedirectUrl
    token = RedirectUrl.split('token=')[1]

    login(allAnswer,myfakeid,token)

    #login(allAnswer,tttfakeid,token)

#发送信息
def login(str,tofakeid,token):
    paras={'type':'1','content':str,'error':'false','imgcode':'','tofakeid':tofakeid,'token':token,'ajax':'1'}# content为你推送的信息，tofakeid为用户的唯一标示id，可在html代码里找到
    req=urllib2.Request('https://mp.weixin.qq.com/cgi-bin/singlesend?t=ajax-response&amp;lang=zh_CN',urllib.urlencode(paras))
    req.add_header('Accept','*/*')
#    req.add_header('Accept-Encoding','gzip,deflate,sdch')
    req.add_header('Accept-Language','zh-CN,zh;q=0.8')
    req.add_header('Connection','keep-alive')
    #req2.add_header('Content-Length','77') 此行代码处理发送数据长度的确认，不要加。是个坑。
    req.add_header('Content-Type','application/x-www-form-urlencoded; charset=UTF-8')
    req.add_header('Host','mp.weixin.qq.com')
    req.add_header('Origin','https://mp.weixin.qq.com')
    req.add_header('Referer','https://mp.weixin.qq.com/cgi-bin/singlesendpage?tofakeid={0}&t=message/send&action=index&quickReplyId=402639012&token={1}&lang=zh_CN'.format(tofakeid,token))
    req.add_header('User-Agent','Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36')
    req.add_header('X-Requested-With','XMLHttpRequest')
    #不加cookie也可发送
    #req2.add_header('Cookie',cookie2)
    ret2=urllib2.urlopen(req)
    print 'x',ret2.read()

#MD5加密
def md5(str):
    import hashlib
    m = hashlib.md5()
    m.update(str)
    return m.hexdigest()

#获取最新微博信息
def weibodata(url,name,dic):
    headers = {'User-Agent': 'Mozilla/5.0 (MSIE 9.0; Windows NT 6.3; WOW64; Trident/7.0; MALNJS; rv:11.0) like Gecko'}
    req = urllib2.Request(url ,urllib.urlencode({}), headers)
    resp = urllib2.urlopen(req).read()
    soup = bs4.BeautifulSoup(resp,"lxml")
    html = requests.get(url).content
    #data = helper.getData(source)
    #content = helper.getContent(data)
    firstpageblog=re.findall('<div class="c" id="M_(?P<dd>[\S]*)">',html)
    #判断是否有置顶微博
    topblog=re.findall('<div class="c" id="(.*?)"><div>.*?<span class="kt"',html)
    if len(topblog) is 0:
        firstblog=re.search('<div class="c" id="M_([\S]*)">',html)
        M_iddic1=firstblog.group(1)
        #发微博的时间
        weibotime = re.search('<span class="ct">(?P<time>.*?)&nbsp',html)
        weibotime = weibotime.group("time")
    else:
        firstblog = "M_"+firstpageblog[1]
        M_iddic1 = firstpageblog[1]
        #发微博的时间
        weibotime = re.findall('<span class="ct">(.*?)&nbsp',html)
        weibotime = weibotime[1]
    if firstblog is not None:
        M_id = "\"M_"+M_iddic1+"\""
        #createinfo1 = re.search('<div class="c" id=%s><div><span class="ctt">(?P<dd>.*?)&nbsp;<a'%M_idhuge,html1)
        if M_iddic1 != dic:
            dic = M_iddic1
            info = re.search('<div class="c" id=%s><div><span class="cmt">(?P<dd>.*?)&nbsp;<a'%M_id,html)
            if info is not None and info.group("dd") == "转发了":

                transedpeople = re.search('%s><div><span class="cmt">转发了&nbsp;<.*?>(?P<transedpeople>.*?)</a>'%M_id,html)
                transedpeople = "@"+transedpeople.group("transedpeople")
                whosweibo =  re.search('id=%s><div><span class="cmt">.*?&nbsp;(?P<dd><a href=.*?>.*?</a>)'%M_id,html)
                whosweibo = whosweibo.group("dd")
                whosweibo = re.sub('(?<=>)(.*?)(?=</a>)', transedpeople, whosweibo)
                weibo = "<a href=\"{0}\">@{1}</a>".format(url,name)+"在"+weibotime+"转发了一条"+whosweibo+"的微博"
                LoginPost(weibo)
                #print info1
                #print M_idhuge
            else:
                #长微博
                longinfo= re.search('%s\'>(?P<dd>.*?)</a>'%M_iddic1,html)
                if longinfo  is not None:
                    longinfo = longinfo.group("dd")
                    #rex = '<div class="c" id=%s><div><span class="cmt">(?P<dd>.*?)\s<a'%M_id
                    #微博有点长 得点进去看
                    longhtml  = 'http://weibo.cn/comment/'+ M_iddic1
                    longhtml = requests.get(longhtml).content
                    longcontent = re.search('<span class="ctt">:(?P<dd>.*?)</span>',longhtml)
                    newweibo = longcontent.group("dd")
                    #是否有@人
                    atpeople = re.search('(?<=<a href=")/(.*?)(?=">)',newweibo)
                    if atpeople is not None:
                        print atpeople.group(0)
                        atpeople = "http://weibo.com" + atpeople.group(0)
                        newweibo = re.sub('(?<=<a href=")/(.*?)(?=">)', atpeople, newweibo)
                    print atpeople
                else:
                    newweibo = re.search('<div class="c" id=%s><div><span class="ctt">(?P<dd1>.*?)</span>'%M_id,html)
                    newweibo = newweibo.group("dd1")
                    #是否有@人
                    atpeople = re.search('(?<=<a href=")/(.*?)(?=">)',newweibo)
                    if atpeople is not None:
                        print atpeople.group(0)
                        atpeople = "http://weibo.com" + atpeople.group(0)
                        newweibo = re.sub('(?<=<a href=")/(.*?)(?=">)', atpeople, newweibo)
                #str = soup2.findAll(class_ = "ctt")[3].contents[0]
                #req=urllib2.Request("http://weibo.cn/attitude/"+M_id+"/add?uid="+uid+"&rl=0&st="+st,None,headers)
                #urllib2.urlopen(req)
                time.sleep(1)
                weibo = "<a href=\"{0}\">@{1}</a>".format(url,name)+"在"+weibotime+"发了一条微博：\n" + urllib.unquote(newweibo).replace('amp;','').replace('<br/>','\n') #如果微博中有超链接需要url解码
                print weibo
                LoginPost(weibo)
    return dic

reload(sys)
sys.setdefaultencoding('utf-8')

#胡歌
hugeurl = 'http://weibo.cn/hu_ge'
dicthuge=''
#林俊杰
jjlinurl = 'http://weibo.cn/jjlin'
dictjjlin=''
#tucao
xuetucaourl = 'http://weibo.cn/xuetucao'
dictxuetucao=''


updatelogtitle = '/::) 2016-03-13更新日志：\n'
updatelog1 = '1.获取含有超链接的最新微博，进行解码，点击可正常跳转到指定链接;\n'
updatelog2 = '2.获取最新微博中有@其他人时，点击能正常跳转到此人微博地址\n'
updatelog3 = '3.转发微博，点击被转发人，可以跳转到此人微博地址\n'
updatelog = updatelogtitle+updatelog1+updatelog2+updatelog3
#LoginPost(updatelog);

while True:
    dicthuge = weibodata(hugeurl,"胡歌",dicthuge)
    dictjjlin = weibodata(jjlinurl,"林俊杰",dictjjlin)
    #dictxuetucao = weibodata(xuetucaourl,"小野妹子学吐槽",dictxuetucao)
    time.sleep(7200)
