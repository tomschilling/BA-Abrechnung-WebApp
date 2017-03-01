<%@ Page Language="VB" Inherits="System.Web.Mvc.ViewPage(Of Mitgliederdatenbank.Trainingseinheit)" %>

<!DOCTYPE html>

<html>
<head id="Head1"  runat="server">
    <title>Trainingseinheit "<%=Html.Encode(Model.IdPk)%>" löschen - Mitgliederdatenbank</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" href="~/Content/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="~/Content/dashboard.css" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
        
    <script type="text/javascript">
            $(window).load(function () {
                $('#dlgWirklich').modal('show');
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
          <a class="navbar-brand" href="#">Körperbau - Mitgliederdatenbank</a>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav navbar-right">
            <li><a href="#">Benutzer</a></li>
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
             <li class="active"><a href="#">Trainingseinheiten</a></li>
            <li><a href="#">Mitglieder</a></li>
            <li><a href="#">Abrechnungen</a></li>
          </ul>
        </div>   
       <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
           <% Using Html.BeginForm()%>
          <h1 class="page-header alert "></h1>
          <div class="table-responsive">
                       <div class="form-group">
                            <%= Html.Hidden("IdPk")%>
                            <%= Html.Hidden("Version")%>
                        </div> 
               
           <div id="dlgWirklich" class="modal fade" role="dialog">
            <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <%=Html.ActionLink("x", "Index", Nothing, New With {.class="btn close"}) %>
                    <h4 class="modal-title">Trainingseinheit "<%=Html.Encode(Model.IdPk)%>" löschen</h4>
                </div>
                <div class="modal-body">
                    <p>Möchten Sie die Trainingseinheit "<%=Html.Encode(Model.IdPk)%>" wirklich löschen?</p>
                </div>
                <div class="modal-footer">
                    <%=Html.ActionLink("Nein", "Index", Nothing, New With {.class="btn btn-default"}) %>
                    <input type="submit" class="btn btn-danger " value="Ja" />
                </div>
            </div>

            </div>

        </div>
        <% End Using%>
    </div>
              </div> 
</body>
</html>
