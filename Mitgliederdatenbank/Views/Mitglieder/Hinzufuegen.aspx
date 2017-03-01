<%@ Page Language="VB" Inherits="System.Web.Mvc.ViewPage(Of Mitgliederdatenbank.Mitglied)" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Körperbau - Mitgliederdatenbank</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" href="~/Content/bootstrap.min.css" type="text/css" />
     <link rel="stylesheet" href="~/Content/dashboard.css" type="text/css" />
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    
       <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
  <link rel="stylesheet" href="/resources/demos/style.css"> 
    
    <script>
         jQuery(function ($) {
             $.datepicker.regional['de'] = {
                 clearText: 'löschen', clearStatus: 'aktuelles Datum löschen',
                 closeText: 'schließen', closeStatus: 'ohne Änderungen schließen',
                 prevText: '<zurück', prevStatus: 'letzten Monat zeigen',
                 nextText: 'Vor>', nextStatus: 'nächsten Monat zeigen',
                 currentText: 'heute', currentStatus: '',
                 monthNames: ['Januar', 'Februar', 'März', 'April', 'Mai', 'Juni',
                 'Juli', 'August', 'September', 'Oktober', 'November', 'Dezember'],
                 monthNamesShort: ['Jan', 'Feb', 'Mär', 'Apr', 'Mai', 'Jun',
                 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dez'],
                 monthStatus: 'anderen Monat anzeigen', yearStatus: 'anderes Jahr anzeigen',
                 weekHeader: 'Wo', weekStatus: 'Woche des Monats',
                 dayNames: ['Sonntag', 'Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag'],
                 dayNamesShort: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
                 dayNamesMin: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
                 dayStatus: 'Setze DD als ersten Wochentag', dateStatus: 'Wähle D, M d',
                 dateFormat: 'dd.mm.yy', firstDay: 1,
                 initStatus: 'Wähle ein Datum', isRTL: false
             };
             $.datepicker.setDefaults($.datepicker.regional['de']);
         });
         $(function () {
             $("#GebDatum").datepicker();
             $.datepicker.setDefaults($.datepicker.regional["de"]);
             $("#Datum").datepicker();
             $.datepicker.setDefaults($.datepicker.regional["de"]);
         });
  </script>
</head>
<body>

     <nav class="navbar navbar-inverse navbar-fixed-top">
      <div class="container-fluid">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="~/Trainingseinheiten/Index.html">Körperbau - Mitgliederdatenbank</a>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav navbar-right">
             <li><%= Html.RouteLink("Benutzer", New With {.action = "Index", .controller = "Benutzer"})%></li>
            <li><a href="#">Einstellung</a></li>
            <li><a href="#">Hilfe</a></li>
          </ul>
          <form class="navbar-form navbar-right">
            <input type="text" class="form-control" placeholder="Suche...">
          </form>
        </div>
      </div>
    </nav>

    <p></p>
    <div class="container-fluid">
      <div class="row">
       <div class="col-sm-3 col-md-2 sidebar">
          <ul class="nav nav-sidebar navbar-brand navbar-btn">
             <li><%= Html.RouteLink("Trainingseinheiten", New With {.action = "Index", .controller = "Trainingseinheiten"})%></li> 
            <li class="active"><%= Html.RouteLink("Mitglieder", New With {.action = "Index", .controller = "Mitglieder"})%></li>
          </ul>
        </div> 
         
       <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
           <% Using Html.BeginForm()%>
          <h1 class="page-header alert ">Neues Mitglied</h1>
           

                <div class="row">
                     <div class="col-sm-3">
                        <div class="form-group">
                            
                            <label class="control-label" for="Vorname">Vorname</label>
                            <%= Html.TextBox("Vorname", Nothing, New With {.class = "form-control", .placeholder = "Max"})%>
                            <%= Html.ValidationMessage("Vorname")%>
                        </div>
                     </div> 
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label" for="Nachname">Nachname</label>
                            <%= Html.TextBox("Name", Nothing, New With {.class = "form-control", .placeholder = "Mustermann"})%>
                            <%= Html.ValidationMessage("Nachname")%>
                        </div>
                     </div>
                     <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label" for="GebDatum">Geburtsdatum</label>
                            <%= Html.ValidationMessage("GebDatum")%>
                            <%= Html.TextBox("GebDatum", Nothing, New With {.class = "form-control", .placeholder = "01.01.2001"})%>
                        </div>
                    </div>
                </div>
               
                 <div class="row">
                     <div class="col-sm-3">
                        <div class="form-group">
                            
                            <label class="control-label" for="StraßeNr">Straße/Nr</label>
                            <%= Html.TextBox("StraßeNr", Nothing, New With {.class = "form-control", .placeholder = "Musterstraße 5"})%>
                            <%= Html.ValidationMessage("StraßeNr")%>
                        </div>
                     </div> 
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label" for="PlzOrt">PLZ/Ort</label>
                            <%= Html.TextBox("PlzOrt", Nothing, New With {.class = "form-control", .placeholder = "12345 Musterstadt"})%>
                            <%= Html.ValidationMessage("PlzOrt")%>
                        </div>
                     </div>
                </div>

                  <div class="row">
                     <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label" for="Email">E-Mail</label>
                            <%= Html.TextBox("Email", Nothing, New With {.class = "form-control", .placeholder = "mustermann@muster.de"})%>
                            <%= Html.ValidationMessage("Email")%>
                        </div>
                     </div> 
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label" for="Tel">Telefon</label>
                            <%= Html.TextBox("Tel", Nothing, New With {.class = "form-control", .placeholder = "030 1234567"})%>
                            <%= Html.ValidationMessage("Tel")%>
                        </div>
                     </div>
                </div>
                
                <div class="row">
                   <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label" for="Datum">Mitgliedsdatum</label>
                            <%= Html.ValidationMessage("Datum")%>
                            <%= Html.TextBox("Datum", Date.Now.ToString("D"), New With {.class = "form-control"})%>
                        </div>
                    </div>
                 </div> 
                 &nbsp;
                <div class="row">
                        <%=Html.ActionLink("Zurück", "Index", Nothing, New With {.class="btn btn-default"}) %>
                        <input type="submit" class="btn btn-primary" value="Speichern" />
                    </div> 
            
                <% End Using  %>
           </div>

            </div>
        </div> 
</body>
</html>
