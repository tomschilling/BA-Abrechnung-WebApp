Public Class BenutzerListe

    Private mlstBenutzer As List(Of Benutzer)
    Public Sub New()
        mlstBenutzer = New List(Of Benutzer)
    End Sub

    Public Sub New(plstBenutzer As List(Of Benutzer))
        mlstBenutzer = plstBenutzer
    End Sub

    Public Property Benutzer() As List(Of Benutzer)
        Get
            Return mlstBenutzer
        End Get
        Set(ByVal value As List(Of Benutzer))
            mlstBenutzer = value
        End Set
    End Property

    Public Sub Hinzufuegen(pBenutzer As Benutzer)
        mlstBenutzer.Add(pBenutzer)
    End Sub

    Public Sub Leeren()
        mlstBenutzer.Clear()
    End Sub

End Class
