<%@ Page Language="VB" Inherits="System.Web.Mvc.ViewPage(Of Mitgliederdatenbank.Trainingseinheitenliste)" %>

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
            <li>&nbsp;</li>
             <li class="active"><%= Html.RouteLink("Trainingseinheiten", New With {.action = "Index", .controller = "Trainingseinheiten"})%></li> 
            <li><%= Html.RouteLink("Mitglieder", New With {.action = "Index", .controller = "Mitglieder"})%></li>
          </ul>
        </div>   
       <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
          <h1 class="page-header alert ">Alle Trainingseinheiten</h1>
          <div class="table-responsive">
             <%= Html.ActionLink("Hinzufügen", "Hinzufuegen", Nothing, New With{.class="btn btn-primary", .role="button"}) %>
              <p></p>
               <table class="table table-striped table-bordered">
                    <tr>
                        <th>ID</th>
                        <th>Mitglied</th>
                        <th>Trainer</th>
                        <th>Datum</th>
                    </tr>
                <%For Each train In Model.Trainingseinheiten%>
                    <tr>
                        <td><%= train.IdPk%></td>
                        <td><%= train.MitName%></td>
                        <td><%= train.BenName%></td>
                        <td><%= train.Datum.ToString("D")%></td>
                        <td>
                            <%= Html.ActionLink("Bearbeiten", "Bearbeiten", New With {.id = train.IdPk}, New With {.class = "btn btn-success", .role = "button"})%>
                            <%= Html.ActionLink("Löschen", "Loeschen", New With {.id = train.IdPk}, New With {.class = "btn btn-danger", .role = "button"})%>  
                             </td>
                    </tr>
                <%Next%>
                </table>
                  </div> 
                  
              </div> 
          </div>
        </div>
</body>
</html>
