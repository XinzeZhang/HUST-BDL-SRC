var returnValue="";
function getDate(field,strBil){
   var url ="js/CalendarCn.htm?field=" + field ;
   var calWin = window.showModalDialog (url,window,"dialogHeight: 250px; dialogWidth: 430px;  center: Yes; help: No; resizable: No; status: No");
	field.value=returnValue;
}
