function jsp_selecttab(pdiv){
    var sclassbefore=pdiv.className.substring(0,pdiv.className.indexOf('_')); 
    var itab=0;
    var btab=false;
    var i=0;
    var pset=document.getElementsByName(pdiv.id);
    for(var i=0;i<pset.length;i++){
        if(pset[i].id==pdiv.id){
            if(!btab){itab++;}
            if(pset[i]==pdiv){
                pset[i].className=sclassbefore+'_sel';
                btab=true;
            }
            else{
                pset[i].className=sclassbefore+'_nor';
            }
        }
    }
    var itemp=0;
    var pset=document.getElementsByName(pdiv.id+'_content');
    for(var i=0;i<pset.length;i++){
        if(pset[i].id==(pdiv.id+'_content')){
            itemp++;
            if(itemp==itab){
                pset[i].className=sclassbefore+'_content_sel';
            }
            else{
                pset[i].className=sclassbefore+'_content_nor';
            }
        }
    }
};


var lunarInfo=new Array(
0x04bd8,0x04ae0,0x0a570,0x054d5,0x0d260,0x0d950,0x16554,0x056a0,0x09ad0,0x055d2,
0x04ae0,0x0a5b6,0x0a4d0,0x0d250,0x1d255,0x0b540,0x0d6a0,0x0ada2,0x095b0,0x14977,
0x04970,0x0a4b0,0x0b4b5,0x06a50,0x06d40,0x1ab54,0x02b60,0x09570,0x052f2,0x04970,
0x06566,0x0d4a0,0x0ea50,0x06e95,0x05ad0,0x02b60,0x186e3,0x092e0,0x1c8d7,0x0c950,
0x0d4a0,0x1d8a6,0x0b550,0x056a0,0x1a5b4,0x025d0,0x092d0,0x0d2b2,0x0a950,0x0b557,
0x06ca0,0x0b550,0x15355,0x04da0,0x0a5d0,0x14573,0x052d0,0x0a9a8,0x0e950,0x06aa0,
0x0aea6,0x0ab50,0x04b60,0x0aae4,0x0a570,0x05260,0x0f263,0x0d950,0x05b57,0x056a0,
0x096d0,0x04dd5,0x04ad0,0x0a4d0,0x0d4d4,0x0d250,0x0d558,0x0b540,0x0b5a0,0x195a6,
0x095b0,0x049b0,0x0a974,0x0a4b0,0x0b27a,0x06a50,0x06d40,0x0af46,0x0ab60,0x09570,
0x04af5,0x04970,0x064b0,0x074a3,0x0ea50,0x06b58,0x055c0,0x0ab60,0x096d5,0x092e0,
0x0c960,0x0d954,0x0d4a0,0x0da50,0x07552,0x056a0,0x0abb7,0x025d0,0x092d0,0x0cab5,
0x0a950,0x0b4a0,0x0baa4,0x0ad50,0x055d9,0x04ba0,0x0a5b0,0x15176,0x052b0,0x0a930,
0x07954,0x06aa0,0x0ad50,0x05b52,0x04b60,0x0a6e6,0x0a4e0,0x0d260,0x0ea65,0x0d530,
0x05aa0,0x076a3,0x096d0,0x04bd7,0x04ad0,0x0a4d0,0x1d0b6,0x0d250,0x0d520,0x0dd45,
0x0b5a0,0x056d0,0x055b2,0x049b0,0x0a577,0x0a4b0,0x0aa50,0x1b255,0x06d20,0x0ada0)
var Animals=new Array("鼠","牛","虎","兔","龙","蛇","马","羊","猴","鸡","狗","猪");
var Gan=new Array("甲","乙","丙","丁","戊","己","庚","辛","壬","癸");
var Zhi=new Array("子","丑","寅","卯","辰","巳","午","未","申","酉","戌","亥");
var now = new Date();
var SY = now.getYear(); 
var SM = now.getMonth();
var SD = now.getDate();
 
function cyclical(num) { return(Gan[num%10]+Zhi[num%12]) } //==== 传入 offset 传回干支, 0=甲子
//==== 传回农历 y年的总天数
function lYearDays(y) {
   var i, sum = 348
   for(i=0x8000; i>0x8; i>>=1) sum += (lunarInfo[y-1900] & i)? 1: 0
   return(sum+leapDays(y))
}
 
//==== 传回农历 y年闰月的天数
function leapDays(y) {
   if(leapMonth(y))  return((lunarInfo[y-1900] & 0x10000)? 30: 29)
   else return(0)
}
 
//==== 传回农历 y年闰哪个月 1-12 , 没闰传回 0
function leapMonth(y) { return(lunarInfo[y-1900] & 0xf)}
 
//====================================== 传回农历 y年m月的总天数
function monthDays(y,m) { return( (lunarInfo[y-1900] & (0x10000>>m))? 30: 29 )}
 
//==== 算出农历, 传入日期物件, 传回农历日期物件
//     该物件属性有 .year .month .day .isLeap .yearCyl .dayCyl .monCyl
function Lunar(objDate) {
   var i, leap=0, temp=0
   var baseDate = new Date(1900,0,31)
   var offset   = (objDate - baseDate)/86400000
 
   this.dayCyl = offset + 40
   this.monCyl = 14
 
   for(i=1900; i<2050 && offset>0; i++) {
      temp = lYearDays(i)
      offset -= temp
      this.monCyl += 12
   }
   if(offset<0) {
      offset += temp;
      i--;
      this.monCyl -= 12
   }
 
   this.year = i
   this.yearCyl = i-1864
 
   leap = leapMonth(i) //闰哪个月
   this.isLeap = false
 
   for(i=1; i<13 && offset>0; i++) {
      //闰月
      if(leap>0 && i==(leap+1) && this.isLeap==false)
         { --i; this.isLeap = true; temp = leapDays(this.year); }
      else
         { temp = monthDays(this.year, i); }
 
      //解除闰月
      if(this.isLeap==true && i==(leap+1)) this.isLeap = false
 
      offset -= temp
      if(this.isLeap == false) this.monCyl ++
   }
 
   if(offset==0 && leap>0 && i==leap+1)
      if(this.isLeap)
         { this.isLeap = false; }
      else
         { this.isLeap = true; --i; --this.monCyl;}
 
   if(offset<0){ offset += temp; --i; --this.monCyl; }
 
   this.month = i
   this.day = offset + 1
}
 
function YYMMDD(){ 
    var cl = '<font color="green" STYLE="font-size:13pt;">'; 
    if (now.getDay() == 0) cl = '<font color="#c00000" STYLE="font-size:13pt;">'; 
    if (now.getDay() == 6) cl = '<font color="green" STYLE="font-size:13pt;">';
    return(SY+'年'+(SM+1)+'月'); 
}
function weekday(){ 
    var day = new Array("星期日","星期一","星期二","星期三","星期四","星期五","星期六");
    var cl = '<font color="green" STYLE="font-size:9pt;">'; 
    if (now.getDay() == 0) cl = '<font color="green" STYLE="font-size:9pt;">';
    if (now.getDay() == 6) cl = '<font color="red" STYLE="font-size:9pt;">'; 
    return(day[now.getDay()]); 
}
//==== 中文日期
function cDay(m,d){
var nStr1 = new Array('日','一','二','三','四','五','六','七','八','九','十');
var nStr2 = new Array('初','十','廿','卅','　');
var s;
if (m>10){s = '十'+nStr1[m-10]} else {s = nStr1[m]} s += '月'
switch (d) {
  case 10:s += '初十'; break;
  case 20:s += '二十'; break;
  case 30:s += '三十'; break;
  default:s += nStr2[Math.floor(d/10)]; s += nStr1[d%10];
}
return(s);
}
function solarDay1(){
    var sDObj = new Date(SY,SM,SD);
    var lDObj = new Lunar(sDObj);
    var cl = '<font color="#9933CC" STYLE="font-size:9pt;">'; 
    var tt = '【'+Animals[(SY-4)%12]+'】'+cyclical(lDObj.monCyl)+'月 '+cyclical(lDObj.dayCyl++)+'日' ;
    return(cl+tt+'</font>');
}
function solarDay2(){
    var sDObj = new Date(SY,SM,SD);
    var lDObj = new Lunar(sDObj);
    //var cl = '<font color="green" STYLE="font-size:9pt;">'; 
    //农历BB'+(cld[d].isLeap?'闰 ':' ')+cld[d].lMonth+' 月 '+cld[d].lDay+' 日
    var tt = cyclical(SY-1900+36)+'年 '+cDay(lDObj.month,lDObj.day);
    return(tt);
}
function solarDay3(){
var sTermInfo = new Array(0,21208,42467,63836,85337,107014,128867,150921,173149,195551,218072,240693,263343,285989,308563,331033,353350,375494,397447,419210,440795,462224,483532,504758)
var solarTerm = new Array("小寒","大寒","立春","雨水","惊蛰","春分","清明","谷雨","立夏","小满","芒种","夏至","小暑","大暑","立秋","处暑","白露","秋分","寒露","霜降","立冬","小雪","大雪","冬至")
var lFtv = new Array("0101*春节","0115 元宵节","0505 端午节","0707 七夕情人节","0715 中元节","0815 中秋节","0909 重阳节","1208 腊八节","1224 小年","0100*除夕")
var sFtv = new Array("0101*元旦","0214 情人节","0308 妇女节","0309 偶今天又长一岁拉","0312 植树节","0315 消费者权益日","0401 愚人节","0418 MM的生日","0501 劳动节","0504 青年节","0512 护士节","0601 儿童节","0701 建党节 香港回归纪念","0801 建军节","0808 父亲节","0909 毛席逝世纪念","0910 教师节","0928 孔子诞辰","1001*国庆节",
"1006 老人节","1024 联合国日","1112 孙中山诞辰","1220 澳门回归纪念","1225 圣诞节","1226 毛席诞辰")
 
  var sDObj = new Date(SY,SM,SD);
  var lDObj = new Lunar(sDObj);
  var lDPOS = new Array(3)
  var festival='',solarTerms='',solarFestival='',lunarFestival='',tmp1,tmp2;
  //农历节日 
  for(i in lFtv)
  if(lFtv[i].match(/^(\d{2})(.{2})([\s\*])(.+)$/)) {
   tmp1=Number(RegExp.$1)-lDObj.month
   tmp2=Number(RegExp.$2)-lDObj.day
   if(tmp1==0 && tmp2==0) lunarFestival=RegExp.$4 
  }
  //国历节日
  for(i in sFtv)
  if(sFtv[i].match(/^(\d{2})(\d{2})([\s\*])(.+)$/)){
   tmp1=Number(RegExp.$1)-(SM+1)
   tmp2=Number(RegExp.$2)-SD
   if(tmp1==0 && tmp2==0) solarFestival = RegExp.$4 
  }
  //节气
  tmp1 = new Date((31556925974.7*(SY-1900)+sTermInfo[SM*2+1]*60000)+Date.UTC(1900,0,6,2,5))
  tmp2 = tmp1.getUTCDate()
  if (tmp2==SD) solarTerms = solarTerm[SM*2+1]  
  tmp1 = new Date((31556925974.7*(SY-1900)+sTermInfo[SM*2]*60000)+Date.UTC(1900,0,6,2,5))
  tmp2= tmp1.getUTCDate()
  if (tmp2==SD) solarTerms = solarTerm[SM*2] 
 
  if(solarTerms == '' && solarFestival == '' && lunarFestival == '')
    festival = '';
  else
    festival = '<TABLE WIDTH=100% BORDER=0 CELLPADDING=2 CELLSPACING=0 BGCOLOR="#CCFFCC"><TR><TD align=center><marquee direction=left scrolldelay=120 behavior=alternate>'+
    '<FONT COLOR="#FF33FF" STYLE="font-size:9pt;"><b>'+solarTerms + ' ' + solarFestival + ' ' + lunarFestival+'</b></FONT></marquee></TD>'+
    '</TR></TABLE>';
         
  var cl = '<font color="green" STYLE="font-size:9pt;">';
  return(cl+festival+'</font>');
}
 
//显示当前时间
function CurentTime() 
{ 
    var now = new Date(); 
    var hh = now.getHours(); 
    var mm = now.getMinutes(); 
    var ss = now.getTime() % 60000; 
    ss = (ss - (ss % 1000)) / 1000; 
    var clock = hh+':'; 
    if (mm < 10) clock += '0'; 
    clock += mm+':'; 
    if (ss < 10) clock += '0'; 
    clock += ss; 
    return(clock); 
}
 
function refreshCalendarClock() //
{ 
 document.all.ClockTime.innerHTML = CurentTime(); 
}
//显示当前时间
 
function setCalendar(){
//document.write("<table border='1' cellspacing='3' width='180' bordercolor='#009B00' bgcolor='#FFFFFF' height='110' cellpadding='2'");
//document.write("<tr><td align='center'><b>"+YYMMDD()+"<br><font face='Arial' size='6' color=#FF8040>"+SD+"</font><br>");
document.write(YYMMDD()+SD+"日&nbsp;"+weekday()+"&nbsp;");
//document.write(weekday()+"<br><font id=ClockTime color=red></font>"+"<br></b>");
//document.write(solarDay1())
document.write(solarDay2())
//document.write(solarDay3())
//document.write("</td></tr></table>");
 
}
//测试代码
function dzxr_net()
{
        if (event.button==2)alert(' 欢迎光临钱库网，谢谢^_^! ')
}


 function copyUrl()
   {
    var clipBoardContent=this.location.href;
    window.clipboardData.setData("Text",clipBoardContent);
    alert("复制成功!");
   }
   
   
def =1.4; 
max =5; 
min = .01; 
ext = "em"; 
change = .1; 
function addSize(id) { 
    element = document.getElementById(id).style; 
    if (element.fontSize == "") element.fontSize = def+ext; 
    currsize = parseFloat(element.fontSize); 

	
	    if (currsize <= max) 
        element.fontSize = (currsize+change)+ext; 

} 

function subSize(id) { 
    element = document.getElementById(id).style; 
    if (element.fontSize == "") element.fontSize = def+ext; 
    currsize = parseFloat(element.fontSize); 
    //element.lineHeight=orgdheight
	//element.lineHeight=(--orlt)+"px"
    if (currsize >= min) 
        element.fontSize = (currsize-change)+ext; 

} 