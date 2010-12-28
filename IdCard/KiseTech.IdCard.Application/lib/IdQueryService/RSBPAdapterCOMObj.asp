<HTML>
<BODY>
<TITLE> Testing Delphi ASP </TITLE>
<CENTER>
<H3> You should see the results of your Delphi Active Server method below </H3>
</CENTER>
<HR>
<% Set DelphiASPObj = Server.CreateObject("RBSPAdapter_COM.RSBPAdapterCOMObj") 
   DelphiASPObj.queryCondition="XM='ÕÅÈý'"
   DelphiASPObj.queryType="QueryQGRK"
   response.write DelphiASPObj.queryCondition
%>
<HR>
</BODY>
</HTML>