
   var ws1;
   var ws2;
   var deg_output="";
   var deg_cnt=0;
   function ToggleBasicClicked() {
                try {
                	hosturl = window.document.getElementById("serverip").value;	
                    ws1 = new WebSocket("ws://"+hosturl+":9001",["localSensePush-protocol"]);//连接服务器
					ws1.onopen = function(event){Debug("已经与服务器建立了连接");alert("已经与服务器建立了连接\r\n当前连接状态："+this.readyState);};
					ws1.onmessage = function(event) {
                        if (event.data instanceof Blob) {
                            var blob = event.data;
                            //先把blob进行拆分，第一个字节是标识
                            var newblob = blob.slice(0, 3);
                            //js中的blob没有没有直接读出其数据的方法，通过FileReader来读取相关数据
                            var reader = new FileReader();
                            reader.readAsArrayBuffer(newblob);
                            var msgtype=-1;
                            //  当读取操作成功完成时调用.
                            reader.onload = function (evt) {
                                if (evt.target.readyState == FileReader.DONE) {
                                    var data = evt.target.result;
                                    if(data.byteLength==3)
                                    {
                                        var x = new Uint8Array(data);
                                        if(x[0]==0xCC&&x[1]==0x5F&&x[2]==0x01)//位置信息
                                        {
                                            msgtype = 1;
                                        }
                                        else if(x[0]==0xCC&&x[1]==0x5F&&x[2]==0x05)//电量信息
                                        {
                                            msgtype = 2;
                                        }
										else if(x[0]==0xCC&&x[1]==0x5F&&x[2]==0x03)
										{
											msgtype = 3;
										}
										else if(x[0]==0xCC&&x[1]==0x5F&&x[2]==0x08)
										{
											msgtype = 8;
										}
                                        newblob = blob.slice(3);
                                        reader.readAsArrayBuffer(newblob);
                                    }
                                    else
                                    {
                                        if(msgtype==1)
                                        {
                                            handlePosData(data);
                                        }
                                        else if(msgtype==2)
                                        {
                                            handleCapacityData(data);
                                        }
										else if(msgtype==3)
										{
											handleAlarmInfo(data);
										}

                                    }
                                }
                            };
                        }
                    };
					ws1.onclose = function(event){alert("已经与服务器断开连接\r\n当前连接状态："+this.readyState);};
					ws1.onerror = function(event){alert("WebSocket异常！");};
                } catch (ex) {
                    alert(ex.message);      
				}
        };


   function handlePosData(data)
   {
       var array_data = new Uint8Array(data);
       var tagnum=array_data[0];
       if(tagnum<1) {
           return;
       }
       var offset = 1;
       var pos_array = new Array(tagnum);
       for(var i=0;i<tagnum;i++)
       {
           var pos = new Object();
           pos.id = array_data[offset]*256+array_data[offset+1];
           offset+=2;
           pos.x = array_data[offset]*16777216+array_data[offset+1]*65536+array_data[offset+2]*256+array_data[offset+3];
           if(array_data[offset]&0x80==0x80) {//有符号判断
               pos.x = -(0xffffffff-pos.x+1);
           }
           offset+=4;
           pos.y =array_data[offset]*16777216+array_data[offset+1]*65536+array_data[offset+2]*256+array_data[offset+3];
           if(array_data[offset]&0x80==0x80){//有符号判断
               pos.y = -(0xffffffff-pos.y+1);
           }
           offset+=4;
           pos.z =  array_data[offset]*256+array_data[offset+1];
           offset+=2;
           if(array_data[offset]&0x80==0x80) {
               pos.z = -(0xffff-pos.z+1);
           }
           pos.Indicator=array_data[offset];
           offset+=1;
           pos.capacity = array_data[offset];
           offset+=1;
           pos.sleep=array_data[offset];
           offset+=1;
           pos.timestamp=array_data[offset]*16777216+array_data[offset+1]*65536+array_data[offset+2]*256+array_data[offset+3];
           offset+=4;
           var didian_no = array_data[offset];
           var louceng = array_data[offset+1];
           pos.reserverd = array_data[offset]*256+array_data[offset+1];
           offset+=2;
           pos_array.push(pos);
           var hour = parseInt(pos.timestamp/3600000);
           var min = parseInt((pos.timestamp-hour*3600000)/60);
           Debug("位置数据: id："+ parseInt(pos.id)+" x:"+parseFloat(pos.x)+
           	" y："+parseFloat(pos.y)+" 时间："+hour+":"+min);
       }
   }

   function handleCapacityData(data)
   {
       var array_data = new Uint8Array(data);
       var tagnum=array_data[0];
       if(tagnum<1) {
           return;
       }
       var offset = 1;
       var capacity_array = new Array(tagnum);
       for(var i=0;i<tagnum;i++)
       {
           var capacity = new Object();
           capacity.id = array_data[offset]*256+array_data[offset+1];
           offset+=2;
           capacity.cap = array_data[offset];
           offset+=1;
           capacity.bcharged = array_data[offset];
           offset+=1;
           capacity_array.push(capacity);
       }
   }
   
   function handleAlarmInfo(data)
   {
	   var array_data = new Uint8Array(data);
	   var alarm = new Object;
	   var offset=0;
	   alarm.type=array_data[offset];//0x01 电子围栏报警 0x02表示SOS报警 0x03表示剪断报警 0x04表示消失报警
	   offset+=1;
	   alarm.related_tagid =  array_data[offset]*256+array_data[offset+1];
	   offset+=2;
	    alarm.timestamp = array_data[offset+1]*16777216*16777216*256+array_data[offset+1]*16777216*16777216+array_data[offset+2]*16777216*65536+
	   array_data[offset+3]*16777216*256+array_data[offset+4]*16777216+array_data[offset+5]*65536+
	   array_data[offset+6]*256+array_data[offset+7];
	   offset+=8;
	   var content1="";
	   while(offset<132)
	   {
		   if(array_data[offset]==0){
				break;
		   }
		   var char1 = array_data[offset];
		   var str = '%'+char1.toString(16);
		   content1 = content1.concat(str);
		   offset+=1;
	   }
	   alarm.content = decodeFromGb2312(content1);
      // 交给lanny
       var aa=0;
   }

   function ToggleExtraInfoClicked() {
       try {
           ws2 = new WebSocket("ws://192.168.1.154:9001",["localSensePivate-protocol"]);//连接服务器
           ws2.onopen = function(event){alert("已经与服务器建立了连接\r\n当前连接状态："+this.readyState);};
           ws2.onmessage = function(event) {
               if (event.data instanceof Blob) {
                   var blob = event.data;
                   //先把blob进行拆分，第一个字节是标识
                   var newblob = blob.slice(0, 3);
                   //js中的blob没有没有直接读出其数据的方法，通过FileReader来读取相关数据
                   var reader = new FileReader();
                   reader.readAsArrayBuffer(newblob);
                   var msgtype=-1;
                   //  当读取操作成功完成时调用.
                   reader.onload = function (evt) {
                       if (evt.target.readyState == FileReader.DONE) {
                           var data = evt.target.result;
                           if(data.byteLength==3) {
                               var x = new Uint8Array(data);
                               if(x[0]==0xCC&&x[1]==0x5F&&x[2]==0x06) {
                                   msgtype = 6;
                                   newblob = blob.slice(3);
                                   reader.readAsArrayBuffer(newblob);
                               }
                               else if(x[0]==0xCC&&x[1]==0x5F&&x[2]==0x07) {
                                   msgtype = 7;
                                   newblob = blob.slice(3);
                                   reader.readAsArrayBuffer(newblob);
                               }
							    else if(x[0]==0xCC&&x[1]==0x5F&&x[2]==0x12) {
                                   msgtype = 18;
                                   newblob = blob.slice(3);
                                   reader.readAsArrayBuffer(newblob);
                               }
                           }
                           else {
                                 if(msgtype==6) {
                                   handleDistanceData(data);
                               }
                               else if(msgtype==7) {
                                     handleBaseStData(data);
                                 }
								 else if(msgtype==18){
									handleAlarmSwitch(data);
								}
                           }
                       }
                   };
               }
           };
           ws2.onclose = function(event){alert("已经与服务器断开连接\r\n当前连接状态："+this.readyState);};
           ws2.onerror = function(event){alert("WebSocket异常！");};
       } catch (ex) {
           alert(ex.message);
       }
   };

   //测距信息
  function handleDistanceData(data)
  {
      var array_data = new Uint8Array(data);
      var disData = new Object();
      var offset = 0;
      disData.tagid = array_data[offset]*256+array_data[offset+1];
      offset+=2;
      var basenum=array_data[offset];
      if(basenum<1)
      {
          return;
      }
      offset+=1;
      var dis_array = new Array(basenum);
      for(var i=0;i<basenum;i++)
      {
          var base = new Object();
          base.id = array_data[offset]*256+array_data[offset+1];
          offset+=2;
          base.ranging =  array_data[offset]*256+array_data[offset+1];
          offset+=2;
          base.quality = array_data[offset];
          offset+=1;
          offset+=1;//保留字节
          dis_array.push(base);
      }
      // 交给lanny
      var aa=0;
  }

   //基站状态信息
   function handleBaseStData(data)
   {
       var array_data = new Uint8Array(data);
       var BaseStData = new Object();
       var offset = 0;
       var basenum=array_data[offset];
       if(basenum<1)
       {
           return;
       }
       offset+=1;
       var baseState_array = new Array(basenum);
       for(var i=0;i<basenum;i++)
       {
           var base = new Object();
           base.id = array_data[offset]*256+array_data[offset+1];
           offset+=2;
           base.state =  array_data[offset];
           offset+=1;
           base.x = array_data[offset]*16777216+array_data[offset+1]*65536+array_data[offset+2]*256+array_data[offset+3];
           if(array_data[offset]&0x80==0x80) {//有符号判断
               base.x = -(0xffffffff-base.x+1);
           }

           offset+=4;
           base.y = array_data[offset]*16777216+array_data[offset+1]*65536+array_data[offset+2]*256+array_data[offset+3];
           if(array_data[offset]&0x80==0x80) {//有符号判断
               base.y = -(0xffffffff-base.y+1);
           }
           offset+=4;
           base.z = array_data[offset]*256+array_data[offset+1];
           offset+=2;
           baseState_array.push(base);
       }
   }

     //电子围栏启停状态
   //
   function handleAlarmSwitch(data)
   {
	   var array_data = new Uint8Array(data);
       var alarm_swith = new Object();
       var offset = 0;
	   alarm_swith.on_off = array_data[offset];
	   offset+=1;
	   alarm_swith.timestamp = array_data[offset+0]*16777216*16777216*256+array_data[offset+1]*16777216*16777216+array_data[offset+2]*16777216*65536+
  	   array_data[offset+3]*16777216*256+array_data[offset+4]*16777216+array_data[offset+5]*65536+
  	   array_data[offset+6]*256+array_data[offset+7];
	   //to do lanny
	   var a = 0;
   }
   
   //启动停止电子围栏
   //on_off bool类型，true为启动，false为停止
   function SetAlarmSwitch(on_off)
   {
	   var buffer = new ArrayBuffer(16);
	   var  int8view = new Uint8Array(buffer);   
	   int8view[0]=0xCC;
	   int8view[1]=0x5F;
	   int8view[2]=0x12;
	   if(on_off){
		  int8view[3]= 1;
	   }
	   else{
		  int8view[3]= 0;
	   }
	   for(var i=4; i<14;i++)
	   {
		 int8view[i]=0;
	   }

	   int8view[14]=0xAA;
	   int8view[15]=0xBB;
	   ws2.send(int8view.buffer);
/* 	    var a = new Uint8Array([0xCC,0x5F,0x12,4,5]);
	   ws2.send(a.buffer); */
   }

   function Debug(info)
   {
   	 deg_output+=info+"\r\n";
   	 deg_cnt++;
   	 document.getElementById("dbgout").value = deg_output; 
   	 if(deg_cnt>10)
   	 {
   	 	deg_cnt = 0;
   	 	deg_output="";
   	 }
   }