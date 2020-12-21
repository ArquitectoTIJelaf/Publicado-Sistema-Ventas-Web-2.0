<%@ Control Language="VB" ClassName="WUCPrgss" %>
<script runat="server">
</script>
<asp:UpdateProgress ID="upPWeb" runat="server">
<ProgressTemplate>
 <div id="divFrameWeb" class="Cargar-Backgraound">
	 <div id="divFrame" class="Cargar-Content">
     
         		<div  align="center" 
                id="LoaderAjax" 
                class="bcargar" >
                     <img src="images/cargando.gif" width="60" height="60" /> <br />
                        <div>Espere por favor, estamos procesando su solicitud. El tiempo de procesamiento va depender de su conexion de internet
                        </div>
                </div>
	 </div>
  </div>
</ProgressTemplate>
</asp:UpdateProgress>