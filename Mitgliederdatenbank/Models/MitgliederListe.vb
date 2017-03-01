Public Class MitgliederListe
    Private mlstMitglieder As List(Of Mitglied)
    Public Sub New()
        mlstMitglieder = New List(Of Mitglied)
    End Sub

    Public Sub New(plstMitglieder As List(Of Mitglied))  'Konstruktor der Klasse
        mlstMitglieder = plstMitglieder
    End Sub

    'Eigenschaftenwert = Public

    Public Property Mitglieder() As List(Of Mitglied)
        Get
            Return mlstMitglieder
        End Get
        Set(ByVal value As List(Of Mitglied))
            mlstMitglieder = value
        End Set
    End Property

    'Sub zum hinzufügen eines Mitlieds

    Public Sub Hinzufuegen(pMitglied As Mitglied)
        mlstMitglieder.Add(pMitglied)
    End Sub

    'Sub zum leeren der gesamten Liste

    Public Sub Leeren()
        mlstMitglieder.Clear()
    End Sub

End Class
