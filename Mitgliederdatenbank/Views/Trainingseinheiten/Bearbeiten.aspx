<%@ Page Language="VB" Inherits="System.Web.Mvc.ViewPage(Of Mitgliederdatenbank.Trainingseinheit)" %>

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
           <% Using Html.BeginForm()%>
          <h1 class="page-header alert ">Trainingseinheit "<%= Model.IdPk%>" bearbeiten</h1>
          
                    <div class="row">
                    <div class="form-group">
                            <%= Html.Hidden("IdPk")%>
                            <%= Html.Hidden("Version")%>
                        </div> 
                    </div>  
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <%= Html.LabelFor(Function(model) model.MitIdFk, "Mitgliedsname", New With {.class = "control-label"})%>                            
                            <%= Html.DropDownList("mitIdFk", Nothing, New With {.class = "form-control"})%> 
                            <%= Html.ValidationMessageFor(Function(model) model.MitIdFk)%>
                        </div>
                    </div>
                       <div class="col-sm-3">
                        <div class="form-group">
                            <%= Html.LabelFor(Function(model) model.BenIdFk, "Benutzername", New With {.class = "control-label"})%>                            
                            <%= Html.DropDownList("benIdFk", Nothing, New With {.class = "form-control"})%> 
                            <%= Html.ValidationMessageFor(Function(model) model.BenIdFk)%>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label" for="Datum">Datum</label>
                            <%= Html.ValidationMessage("Datum")%>
                            <%= Html.TextBox("Datum", Date.Now, New With {.class = "form-control"})%>
                        </div>
                    </div>
        
                     &nbsp;
                   <div class="row">
                        <div class="form-group">
                        <%=Html.ActionLink("Zurück", "Index", Nothing, New With {.class="btn btn-default"}) %>
                        <input type="submit" class="btn btn-primary" value="Speichern" />
                    </div> 
                   </div> 
                <% End Using  %>
           </div>

            </div>
        </div>  
</body>
</html>
