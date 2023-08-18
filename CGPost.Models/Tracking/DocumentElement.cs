namespace CGPost.Models.Tracking
{
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class DocumentElementTableName
    {

        private byte indField;

        private uint postaField;

        private string mestoField;

        private string prijemniBrojField;

        private string opisField;

        private DateTime datumField;

        private string nazivField;

        private byte sluzbaField;

        private string idField;

        private byte rowOrderField;

        private string hasChangesField;

        /// <remarks/>
        public byte ind
        {
            get
            {
                return indField;
            }
            set
            {
                indField = value;
            }
        }

        /// <remarks/>
        public uint Posta
        {
            get
            {
                return postaField;
            }
            set
            {
                postaField = value;
            }
        }

        /// <remarks/>
        public string mesto
        {
            get
            {
                return mestoField;
            }
            set
            {
                mestoField = value;
            }
        }

        /// <remarks/>
        public string PrijemniBroj
        {
            get
            {
                return prijemniBrojField;
            }
            set
            {
                prijemniBrojField = value;
            }
        }

        /// <remarks/>
        public string Opis
        {
            get
            {
                return opisField;
            }
            set
            {
                opisField = value;
            }
        }

        /// <remarks/>
        public DateTime datum
        {
            get
            {
                return datumField;
            }
            set
            {
                datumField = value;
            }
        }

        /// <remarks/>
        public string Naziv
        {
            get
            {
                return nazivField;
            }
            set
            {
                nazivField = value;
            }
        }

        /// <remarks/>
        public byte sluzba
        {
            get
            {
                return sluzbaField;
            }
            set
            {
                sluzbaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
        public string id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:xml-msdata")]
        public byte rowOrder
        {
            get
            {
                return rowOrderField;
            }
            set
            {
                rowOrderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
        public string hasChanges
        {
            get
            {
                return hasChangesField;
            }
            set
            {
                hasChangesField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class DocumentElement
    {

        private DocumentElementTableName[] tableNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("TableName")]
        public DocumentElementTableName[] TableName
        {
            get
            {
                return tableNameField;
            }
            set
            {
                tableNameField = value;
            }
        }
    }


}