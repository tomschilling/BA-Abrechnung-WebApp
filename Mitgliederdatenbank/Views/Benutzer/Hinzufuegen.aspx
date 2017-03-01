<%@ Page Language="VB" Inherits="System.Web.Mvc.ViewPage(Of Mitgliederdatenbank.BenutzerListe)" %>

<!DOCTYPE html>

<html>
<head id="Head1"  runat="server">
    <title>Körperbau - Mitgliederdatenbank</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" href="~/Content/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="~/Content/dashboard.css" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
<%Dim rootPath As String = Server.MapPath("~")%>
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
          <a class="navbar-brand" href="#">Körperbau - Mitgliederdatenbank</a>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav navbar-right">
            <li class="active"><%= Html.RouteLink("Benutzer", New With {.action = "Index", .controller = "Benutzer"})%></li>
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
            <li>&nbsp;</li>
             <li><%= Html.RouteLink("Trainingseinheiten", New With {.action = "Index", .controller = "Trainingseinheiten"})%></li> 
            <li><%= Html.RouteLink("Mitglieder", New With {.action = "Index", .controller = "Mitglieder"})%></li>
            <li><%= Html.RouteLink("Abrechnungen", New With {.action = "Index", .controller = "Abrechnungen"})%></li>
          </ul>
        </div> 
         
       <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
           <% Using Html.BeginForm()%>
          <h1 class="page-header alert ">Neuer Benutzer</h1>
           

              
                <div class="row">
                     <div class="col-sm-2">
                        <div class="form-group">
                            <label class="control-label" for="Benutzername">Benutzername</label>
                            <%= Html.TextBox("Benutzername", Nothing, New With {.class = "form-control"})%>
                            <%= Html.ValidationMessage("Benutzername")%>
                        </div>
                     </div> 
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label class="control-label" for="Passwort">Passwort</label>
                            <%= Html.TextBox("Passwort", Nothing, New With {.class = "form-control", .type = "password"})%>
                            <%= Html.ValidationMessage("Passwort")%>
                        </div>
                     </div>
                </div>
                
            <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <%= Html.CheckBox("IstVorgesetzter")%>
                            <label for="Vorgesetzter">Vorgesetzter</label>
                            <%= Html.ValidationMessage("IstVorgesetzter")%>
                        </div>
                        </div>
                 </div> 

                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <%= Html.CheckBox("IstAdmin")%>
                              <label for="Administrator">Administrator</label>
                            <%= Html.ValidationMessage("IstAdmin")%>
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
