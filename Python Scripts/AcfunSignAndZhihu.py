# -- coding: utf-8 --

#A站签到并且通过网页rss获取每日知乎日报瞎扯内容使用微信公众号推送
import gzip
import re
import urllib
import  urllib2
import cookielib
import json,time
import sys
import zlib
import binascii
import datetime,feedparser,bs4
from cStringIO import StringIO

#A站签到-----------------------------------------------------------------------------------------------
def loginAcfun():
    cj=cookielib.LWPCookieJar()
    opener = urllib2.build_opener(urllib2.HTTPCookieProcessor(cj))
    urllib2.install_opener(opener)
    #登陆
    paras = {'username':'username','password':'password'}
    req = urllib2.Request('http://passport.acfun.tv/login.aspx',urllib.urlencode(paras))
    req.add_header('Accept','*/*')
    req.add_header('Accept-Encoding','gzip, deflate')
    req.add_header('Accept-Language','zh-CN,zh;q=0.8')
    req.add_header('Connection','keep-alive')
    req.add_header('Content-Length','31')
    req.add_header('Content-Type','application/x-www-form-urlencoded; charset=UTF-8')
    req.add_header('Host','passport.acfun.tv')
    req.add_header('Origin','http://passport.acfun.tv')
    req.add_header('Referer','http://passport.acfun.tv/login/')
    req.add_header('User-Agent','Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.75 Safari/537.36')
    ret = urllib2.urlopen(req)
    retread = ret.read()
    print gzipData(retread)
    sign()

def sign():
    unixTime = str(int(time.time()*1000)+36000)
    paras1 = {'channel':'0','date':unixTime}
    req1 = urllib2.Request('http://www.acfun.tv/webapi/record/actions/signin?channel=0&date=' + unixTime,urllib.urlencode(paras1))
    req1.add_header('Accept','*/*')
    req1.add_header('Accept-Encoding','gzip, deflate')
    req1.add_header('Accept-Language','zh-CN,zh;q=0.8')
    req1.add_header('Connection','keep-alive')
    req1.add_header('Host','www.acfun.tv')
    req1.add_header('Origin','http://www.acfun.tv')
    req1.add_header('Referer','http://www.acfun.tv/member/')
    req1.add_header('User-Agent','Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.75 Safari/537.36')
    for i in range(0,3,1):
        ret1 = urllib2.urlopen(req1)
        retread1 = ret1.read()
        #print gzipData(retread1)
        tmp = gzipData(retread1)
        tmp = json.loads(tmp)
        if ret1.msg == "OK":
            if tmp['code'] == 410004:
                #logging.info('Already sign')
                break
            else:
                time.sleep(3600)
                continue
            #logging.info('sign success')

#返回数据类型为Gzip，需要解析
def gzipData(str):
    inbuffer = StringIO(str)
    f = gzip.GzipFile(mode="rb", fileobj=inbuffer)
    rdata = f.read()
    return rdata

#微信-------------------------------------------------------------------------------
def LoginPost(allAnswer):
    cj=cookielib.LWPCookieJar()
    opener = urllib2.build_opener(urllib2.HTTPCookieProcessor(cj))
    urllib2.install_opener(opener)

    #登陆
    paras = {'username':'username','pwd':md5("pwd"),'imgcode':'','f':'json'}
    req = urllib2.Request('https://mp.weixin.qq.com/cgi-bin/login?lang=zh_CN',urllib.urlencode(paras))
    req.add_header('Accept','*/*')
    req.add_header('Accept-Encoding','gzip, deflate')
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
    #print  retread
    retcontent = json.loads(retread)
    ErrMsg = retcontent['base_resp']['err_msg']
    #print  "Login status: ",ErrMsg
    RedirectUrl = retcontent['redirect_url']
    #print  "redirect url: ",RedirectUrl
    token = RedirectUrl.split('token=')[1]

    login(allAnswer,myfakeid,token,myquickReplyId)

    login(allAnswer,tttfakeid,token,tttquickReplyId)

def login(str,tofakeid,token,quickReplayId):
    paras={'type':'1','content':str,'lang':'zh_CN','imgcode':'','tofakeid':tofakeid,'token':token,'ajax':'1','random':'0.2760961744289987','f':'json','quickReplyId':quickReplayId}# content为你推送的信息，tofakeid为用户的唯一标示id，可在html代码里找到
    #https://mp.weixin.qq.com/cgi-bin/singlesend?t=ajax-response&f=json&token=612153242&lang=zh_CN
    req=urllib2.Request('https://mp.weixin.qq.com/cgi-bin/singlesend?t=ajax-response&f=json&token={0}&lang=zh_CN'.format(token),urllib.urlencode(paras))
    req.add_header('Accept','application/json, text/javascript, */*; q=0.01')
    req.add_header('Accept-Encoding','gzip,deflate,br')
    req.add_header('Accept-Language','zh-CN,zh;q=0.8')
    req.add_header('Connection','keep-alive')
    #req2.add_header('Content-Length','77') 此行代码处理发送数据长度的确认，不要加。是个坑。
    req.add_header('Content-Type','application/x-www-form-urlencoded; charset=UTF-8')
    req.add_header('Host','mp.weixin.qq.com')
    req.add_header('Origin','https://mp.weixin.qq.com')
    req.add_header('Referer','https://mp.weixin.qq.com/cgi-bin/singlesendpage?t=message/send&action=index&tofakeid={0}&quickReplyId={1}&token={2}&lang=zh_CN'.format(tofakeid,quickReplayId,token))
    req.add_header('User-Agent','Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36')
    req.add_header('X-Requested-With','XMLHttpRequest')
    #不加cookie也可发送
    #req2.add_header('Cookie',cookie2)
    ret2=urllib2.urlopen(req)
    print 'x',ret2.read()

def md5(str):
    import hashlib
    m = hashlib.md5()
    m.update(str)
    return m.hexdigest()


def xiache():
    url = "http://zhihurss.miantiao.me/section/id/2" #网页地址
    answer = ""
    #wp = urllib.urlopen(url) #打开连接
    html = urllib2.urlopen(url).read()
    #html = unicode(html,'gb2312','ignore')
    #content = wp.read() #获取页面内容
    rss = feedparser.parse(url)
    end=""
    title=""
    allAnswer=""
    description = rss.entries[0].description
    rem = re.compile('<p><img(.*?)</p>')
    description = rem.sub('',description)
    soup = bs4.BeautifulSoup(description, "lxml");
    count = len(soup('h2'))
    #是话题还是多个问题？
    countQues = len(soup.findAll(class_="question-title"))
    contentCount = len(soup.findAll(class_="content"))
    #话题
    if countQues is 1:
        title = soup.findAll(class_="question-title")[0].string
        end ="问：" + title  + "\n\n"
        for i in range(0,contentCount,1):
            content = soup.findAll(class_="content")[i].findAll("p")#[0].string
            if len(content)>1:
                for j in range(0,len(content),1):
                     answerAdd = soup.findAll(class_="content")[i].findAll("p")[j].string
                     answer += answerAdd
            else:
                answer = soup.findAll(class_="content")[i].findAll("p")[0].string
                if answer is None:
                    answer = ""
                    title = ""
            allAnswer = "答："+ answer +"\n\n"
            end+=allAnswer
    else:
        for i in range(0,count,1):
            #问题
            if count != 0:
                allAnswer = "\n"
            title = soup.findAll(class_="question-title")[i].string
            allAnswer +="问：" + title + "\n"
			#每个问题都循环遍历一把答案，防止有同一个问题会有多个答案，那么问题只用展示一次就行了
            countAnswer = len(soup.findAll(class_="question")[i].findAll(class_= "answer"))
            for m in range(0,countAnswer,1):
                #print title
                #用户
                user = soup.findAll(class_="question")[i].findAll(class_ = "author")[m].string
                user = user.encode('utf-8').replace("，","") #转码去最后的逗号
                #print user
                #签名
                #bio = soup.findAll(class_ = "bio")[i].string
                #print bio
                #回答
                content = soup.findAll(class_="question")[i].findAll(class_="content")[m].findAll("p")#[0].string
                if len(content)>1:
                    for j in range(0,len(content),1):
                         answerAdd = soup.findAll(class_="question")[i].findAll(class_="content")[m].findAll("p")[j].string
                         answer += answerAdd
                else:
                    if len(soup.findAll(class_="question")[i].findAll(class_="content")[m].findAll("p"))!=0:
                        answer = soup.findAll(class_="question")[i].findAll(class_="content")[m].findAll("p")[0].string
                    else:
                        answer = soup.findAll(class_="question")[i].findAll(class_="content")[m].string.replace("\n","")
                    if answer is None:
                        answer = ""
                        title = ""
                        continue
                #print answer
                allAnswer += "\n" +"答："+ answer +"\n"
                answer=""
                end+=allAnswer
                allAnswer=""
    #print end
    #微信发送消息长度不能超过600字符
    if len(end)>=600:
        sendCount = len(end)/600 + 1
        for i in range(sendCount,0,-1):
            LoginPost(end[:600])
            end = end[600:]
    else:
        LoginPost(end)
		
default_encoding = 'utf-8'
if sys.getdefaultencoding() != default_encoding:
    reload(sys)
    sys.setdefaultencoding(default_encoding)

#接受者的fakeid及quickReplyId  都是写死的
#ziji
myfakeid = 'oI8CEs164CfsmGRomlj5NPHDEdX8'
#ttt
tttfakeid = 'oI8CEsymDyTNUv0TYj1EBp7PA7kI'

myquickReplyId = '402639012'
tttquickReplyId = '402695532'
#每天通过任务计划程序执行一次就行了  不用一直执行
#while 1:
#    d1 = datetime.datetime.now()
#    nowTime = (d1 + datetime.timedelta(hours=8)).strftime("%H")
#    if nowTime == "01":
loginAcfun()
#time.sleep(10)
xiache()
#        time.sleep(86000)
