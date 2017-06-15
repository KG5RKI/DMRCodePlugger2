using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DMRCodePlugger
{
    public partial class Codeplug : Kaitai.KaitaiStruct
    {
        public static Codeplug FromFile(string fileName)
        {
            byte[] file = File.ReadAllBytes(fileName);
            return new Codeplug(new Kaitai.KaitaiStream(file));
        }

        private List<Contact> _contacts;
        private Codeplug m_root;
        private Kaitai.KaitaiStruct m_parent;

        public Codeplug(Kaitai.KaitaiStream io, Kaitai.KaitaiStruct parent = null, Codeplug root = null) : base(io)
        {
            m_parent = parent;
            m_root = root;
            _parse();
        }

        public List<Contact> Contacts { get { return _contacts; } }

        private void _parse()
        {
            m_io.BaseStream.Seek(Contact.fOffset, SeekOrigin.Begin);
            _contacts = new List<Contact>((int)(1000));
            for (var i = 0; i < 1000; i++)
            {
                byte[] stuff = m_io.ReadBytes(Contact.fSize);
                var io___raw_contacts = new Kaitai.KaitaiStream(stuff);
                _contacts.Add(new Contact(io___raw_contacts, this, m_root));
            }
        }
        public void save(string filename)
        {
            updateContacts();

            m_io.BaseStream.Seek(0, SeekOrigin.Begin);
            byte[] data = new byte[m_io.BaseStream.Length];
            this.m_io.BaseStream.Read(data, 0, data.Length);
            File.WriteAllBytes(filename, data);
        }

        private void updateContacts()
        {
            m_io.BaseStream.Seek(Contact.fOffset, SeekOrigin.Begin);
            foreach (Contact cc in _contacts)
            {
                m_io.BaseStream.Write(cc.getData(), 0, Contact.fSize);
            }
        }

        public partial class Contact : Kaitai.KaitaiStruct
        {
            public const int fOffset = 0x61A5;
            public const int fSize = 36;

            public Contact(Kaitai.KaitaiStream io, Codeplug parent = null, Codeplug root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                _parse();
            }

            private void _parse()
            {
                byte _id0 = m_io.ReadU1();
                byte _id2 = m_io.ReadU1();
                byte _id3 = m_io.ReadU1();
                Id = (_id3 << 16) | (_id2 << 8) | (_id0);

                _type = m_io.ReadU1();
                Name = System.Text.Encoding.GetEncoding("UTF-16LE").GetString(m_io.ReadBytes(32));
            }

            
            private int _id;
            private byte _type;
            private string _name;

            private Codeplug m_root;
            private Codeplug m_parent;

            public enum ContactType : byte { None = 0, Group = 0xC1, Private = 0x02};

            public int Id
            {
                get { return _id; }
                set
                {
                    _id = value;
                    //int tmp = ((value >> 16) & 0xFF) | (value & 0xFF00) | ((value << 16) & 0xFF0000);

                    byte[] intBytes = BitConverter.GetBytes(_id);

                    m_io.BaseStream.Seek(0, SeekOrigin.Begin);
                    m_io.BaseStream.Write(intBytes, 0, 3);
                }
            }

            public ContactType Type
            {
                get { return (ContactType)_type; }
                set
                {
                    _type = (byte)value;
                    m_io.BaseStream.Seek(3, SeekOrigin.Begin);
                    m_io.BaseStream.WriteByte(_type);
                }
            }
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value.Replace("\0", string.Empty);
                    m_io.BaseStream.Seek(4, SeekOrigin.Begin);
                    byte[] data = System.Text.Encoding.GetEncoding("UTF-16LE").GetBytes(value);
                    
                    m_io.BaseStream.Write(data, 0, data.Length);
                }
            }

            public byte[] getData()
            {
                m_io.BaseStream.Seek(0, SeekOrigin.Begin);
                byte[] data = new byte[fSize];
                this.m_io.BaseStream.Read(data, 0, data.Length);
                return data;
            }
           
        }

        public partial class Channel : Kaitai.KaitaiStruct
        {
            public const int fOffset = 0x61A5;
            public const int fSize = 36;

            public Channel(Kaitai.KaitaiStream io, Codeplug parent = null, Codeplug root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                _parse();
            }

            private void _parse()
            {
                byte b0 = m_io.ReadU1();
                byte b1 = m_io.ReadU1();

                byte _id3 = m_io.ReadU1();
                Id = (_id3 << 16) | (_id2 << 8) | (_id0);

                _type = m_io.ReadU1();
                Name = System.Text.Encoding.GetEncoding("UTF-16LE").GetString(m_io.ReadBytes(32));
            }


            private int _contactIndex;
            private byte _colorCode;
            private int _timeSlot;
            private int _tot;

            private byte _type;
            private string _name;

            private Codeplug m_root;
            private Codeplug m_parent;

            public enum ChannelType : byte { Digital = 0, Analog = 0x1};

            public byte ColorCode
            {
                get { return _colorCode; }
                set
                {
                    _colorCode = value;
                    byte tmp = (byte)((_colorCode << 4) & 0xF0);

                    m_io.BaseStream.Seek(1, SeekOrigin.Begin);
                    byte tmp2 = m_io.ReadU1();
                    tmp2 = (byte)((tmp2 & 0x0F) | tmp);

                    m_io.BaseStream.Seek(1, SeekOrigin.Begin);
                    m_io.BaseStream.WriteByte(tmp2);
                }
            }

            public int ToT
            {
                get { return _tot; }
                set
                {
                    _tot = value;
                    byte tmp = (byte)(_tot);
                    m_io.BaseStream.Seek(8, SeekOrigin.Begin);
                    m_io.BaseStream.WriteByte(tmp);
                }
            }

            public int TimeSlot
            {
                get { return _timeSlot; }
                set
                {
                    _timeSlot = value;
                    byte tmp = (byte)(_timeSlot != 1 ? 0x04 : 0);

                    m_io.BaseStream.Seek(1, SeekOrigin.Begin);
                    byte tmp2 = m_io.ReadU1();
                    tmp2 = (byte)((tmp2 & (~0x04)) | tmp);

                    m_io.BaseStream.Seek(1, SeekOrigin.Begin);
                    m_io.BaseStream.WriteByte(tmp2);
                }
            }

            public ChannelType Type
            {
                get { return (ChannelType)_type; }
                set
                {
                    _type = (byte)value;
                    m_io.BaseStream.Seek(3, SeekOrigin.Begin);
                    m_io.BaseStream.WriteByte(_type);
                }
            }

            public int ContactIndex
            {
                get { return _contactIndex; }
                set
                {
                    _contactIndex = value;
                    UInt16 tmp = (UInt16)value;

                    byte[] intBytes = BitConverter.GetBytes(tmp);
                    m_io.BaseStream.Seek(6, SeekOrigin.Begin);
                    m_io.BaseStream.Write(intBytes, 0, 2);
                }
            }

            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value.Replace("\0", string.Empty);
                    m_io.BaseStream.Seek(4, SeekOrigin.Begin);
                    byte[] data = System.Text.Encoding.GetEncoding("UTF-16LE").GetBytes(value);

                    m_io.BaseStream.Write(data, 0, data.Length);
                }
            }

            public byte[] getData()
            {
                m_io.BaseStream.Seek(0, SeekOrigin.Begin);
                byte[] data = new byte[fSize];
                this.m_io.BaseStream.Read(data, 0, data.Length);
                return data;
            }

        }
    }
}
