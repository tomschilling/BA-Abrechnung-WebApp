Imports System.ComponentModel.DataAnnotations

'Am Beispiel des Mitglieds wird die Vorgehensweise und der Aufbau jeder verwendeten Model-Klasse in diesem Projekt gezeigt
'Die "Listen"-Model-Klassen werden an der MitgliederListe kommentiert, da sie im Aufbau alle identisch sind
Public Class Benutzer
    Private mintIdPk As Integer
    Private mstrName As String
    Private mstrPasswort As String
    Private mbolIstAdmin As Boolean
    Private mbolIstVorgesetzter As Boolean
    Private mbytVersion As Byte()

    Public Sub New(pBenutzerEntity As BenutzerEntity)
        mintIdPk = pBenutzerEntity.benIdPK
        mstrName = pBenutzerEntity.benName
        mstrPasswort = pBenutzerEntity.benPasswort
        mbolIstAdmin = pBenutzerEntity.benIstAdmin
        mbolIstVorgesetzter = pBenutzerEntity.benIstVorgesetzter
        mbytVersion = pBenutzerEntity.benVersion
    End Sub

    Public Sub New()
        mintIdPk = Nothing
        mstrName = String.Empty
        mstrPasswort = String.Empty
        mbolIstAdmin = False
        mbolIstVorgesetzter = False
        mbytVersion = Nothing
    End Sub

    Public Sub New(pintIdPk As Integer, pstrName As String, pstrPasswort As String, pbolIstAdmin As Boolean, pbolIstVorgesetzter As Boolean, pstrVersion As String)
        mintIdPk = pintIdPk
        mstrName = pstrName
        mstrPasswort = pstrPasswort
        mbolIstAdmin = pbolIstAdmin
        mbolIstVorgesetzter = pbolIstVorgesetzter
        mbytVersion = Convert.FromBase64String(pstrVersion)
    End Sub

    Public Property IdPk() As Long
        Get
            Return mintIdPk
        End Get
        Set(value As Long)
            mintIdPk = value
        End Set
    End Property

    ' Bei der Version muss für die Darstellung in der View das Byte Array in Unicode umgewandelt werden
    Public Property Version As String
        Get
            If IsNothing(mbytVersion) Then
                Return Nothing
            Else
                Return Convert.ToBase64String(mbytVersion)
            End If
        End Get
        Set(value As String)
            mbytVersion = Convert.FromBase64String(value)
        End Set
    End Property


    <Required(ErrorMessage:="Benutzer müssen ausgewählt werden")>
    <StringLength(25, ErrorMessage:="Maximallänge überschritten")>
    Public Property Benutzername As String
        Get
            Return mstrName
        End Get
        Set(value As String)
            mstrName = value
        End Set
    End Property

    Public Property Passwort As String
        Get
            Return mstrPasswort
        End Get
        Set(value As String)
            mstrPasswort = value
        End Set
    End Property

    Public Property IstAdmin As Boolean
        Get
            Return mbolIstAdmin
        End Get
        Set(value As Boolean)
            mbolIstAdmin = value
        End Set
    End Property

    Public Property IstVorgesetzter As Boolean
        Get
            Return mbolIstVorgesetzter
        End Get
        Set(value As Boolean)
            mbolIstVorgesetzter = value
        End Set
    End Property

    Public Function gibAlsBenutzerEntity() As BenutzerEntity
        Dim benE As BenutzerEntity
        benE = New BenutzerEntity
        benE.benIdPk = mintIdPk
        benE.benName = mstrName
        benE.benPasswort = mstrPasswort
        benE.benIstAdmin = mbolIstAdmin
        benE.benIstVorgesetzter = mbolIstVorgesetzter
        benE.benVersion = mbytVersion
        Return benE
    End Function

End Class
