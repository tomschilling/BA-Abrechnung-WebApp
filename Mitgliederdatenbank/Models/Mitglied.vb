Imports System.ComponentModel.DataAnnotations

'Am Beispiel des Mitglieds wird die Vorgehensweise und der Aufbau jeder verwendeten Model-Klasse in diesem Projekt gezeigt 
'Die "Listen"-Model-Klassen werden an der MitgliederListe kommentiert, da sie im Aufbau alle identisch sind
Public Class Mitglied
    Private mintIdPk As Integer                         'alle Variabeln deklarieren
    Private mstrName As String
    Private mstrVorname As String
    Private mstrStraßeNr As String
    Private mstrPlzOrt As String
    Private mstrTel As String
    Private mstrEmail As String
    Private mdatGebDatum As Date
    Private mdatMtglDatum As Date
    Private mbytVersion As Byte()                       ' Die Version ist in der Klasse Mitglied als Datentyp ein Byte Array

    'Verabeitet das Mitglied als Entity-Objekt in eine neue Model-Klasse
    Public Sub New(pMitgliederEntity As MitgliedEntity)
        mintIdPk = pMitgliederEntity.mitIdPk
        mstrName = pMitgliederEntity.mitName
        mstrVorname = pMitgliederEntity.mitVorname
        mstrStraßeNr = pMitgliederEntity.mitStraßeNr
        mstrPlzOrt = pMitgliederEntity.mitPlzOrt
        mstrTel = pMitgliederEntity.mitTel
        mstrEmail = pMitgliederEntity.mitEmail
        mdatGebDatum = pMitgliederEntity.mitGebDatum
        mdatMtglDatum = pMitgliederEntity.mitMtglDatum
        mbytVersion = pMitgliederEntity.mitVersion
    End Sub

    Public Sub New()
        mintIdPk = Nothing
        mstrName = String.Empty
        mstrVorname = String.Empty
        mstrStraßeNr = String.Empty
        mstrPlzOrt = String.Empty
        mstrTel = String.Empty
        mstrEmail = String.Empty
        mdatGebDatum = #1/1/1900#
        mdatMtglDatum = Date.Now
        mbytVersion = Nothing
    End Sub

    'Konstruktor der Klasse
    Public Sub New(pintIdPk As Integer, pstrName As String, pstrVorname As String, pstrStraßeNr As String, pstrPlzOrt As String, pstrTel As String, pstrEmail As String, pdatGebDatum As Date, pdatMtglDatum As Date, pstrVersion As String)
        mintIdPk = pintIdPk
        mstrName = pstrName
        mstrVorname = pstrVorname
        mstrStraßeNr = pstrStraßeNr
        mstrPlzOrt = pstrPlzOrt
        mstrTel = pstrTel
        mstrEmail = pstrEmail
        mdatGebDatum = pdatGebDatum
        mdatMtglDatum = pdatMtglDatum
        mbytVersion = Convert.FromBase64String(pstrVersion)
    End Sub

    'Eigenschaften des Zugriffes werden vergeben alle Public. Geht auch ReadOnly und WriteOnly 

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
    Public Property Name As String
        Get
            Return mstrName
        End Get
        Set(value As String)
            mstrName = value
        End Set
    End Property

    Public Property Vorname As String
        Get
            Return mstrVorname
        End Get
        Set(value As String)
            mstrVorname = value
        End Set
    End Property

    Public Property StraßeNr As String
        Get
            Return mstrStraßeNr
        End Get
        Set(value As String)
            mstrStraßeNr = value
        End Set
    End Property

    Public Property PlzOrt As String
        Get
            Return mstrPlzOrt
        End Get
        Set(value As String)
            mstrPlzOrt = value
        End Set
    End Property

    Public Property Tel As String
        Get
            Return mstrTel
        End Get
        Set(value As String)
            mstrTel = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return mstrEmail
        End Get
        Set(value As String)
            mstrEmail = value
        End Set
    End Property

    Public Property GebDatum As Date
        Get
            Return mdatGebDatum
        End Get
        Set(value As Date)
            mdatGebDatum = value
        End Set
    End Property

    Public Property MtglDatum As Date
        Get
            Return mdatMtglDatum
        End Get
        Set(value As Date)
            mdatMtglDatum = value
        End Set
    End Property

    'Das Mitglied als Model-Klasse wird in ein Entity-Objekt umgewandelt
    Public Function gibAlsMitgliederEntity() As MitgliedEntity
        Dim mitE As MitgliedEntity
        mitE = New MitgliedEntity
        mitE.mitIdPk = mintIdPk
        mitE.mitName = mstrName
        mitE.mitVorname = mstrVorname
        mitE.mitStraßeNr = mstrStraßeNr
        mitE.mitPlzOrt = mstrPlzOrt
        mitE.mitTel = mstrTel
        mitE.mitEmail = mstrEmail
        mitE.mitGebDatum = mdatGebDatum
        mitE.mitMtglDatum = mdatMtglDatum
        mitE.mitVersion = mbytVersion
        Return mitE
    End Function
End Class
