using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BaseCorePlugin.Model
{
    class MasterModel
    {
        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _effect = string.Empty;
        public string Effect
        {
            get => _effect;
            set => this._effect = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        private DetailModel _value = new DetailModel();
        public DetailModel Value
        {
            get => _value;
            set => this._value = value == null ? null : value;
        }

        public MasterModel(Item1 source)
        {
            this.Effect = source.Effect;
            this.Value.Name = source.Value[0].Name;
            this.Value.Type = source.Value[0].Type;
            this.Value.Target = source.Value[0].Target;
            this.Value.Origin = source.Value[0].Origin;
            this.Value.Effect = source.Value[0].Effect;
            this.Value.X = source.Value[0].X;
            this.Value.Y = source.Value[0].Y;
            this.Value.Width = source.Value[0].Width;
            this.Value.Height = source.Value[0].Height;
            this.Value.Group = source.Value[0].Group;
            this.Value.Alpha = source.Value[0].Alpha;
        }
    }

    class DetailModel
    {
        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => this._name = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _type = string.Empty;
        public string Type
        {
            get => _type;
            set => this._type = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _target = string.Empty;
        public string Target
        {
            get => _target;
            set => this._target = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _origin = string.Empty;
        public string Origin
        {
            get => _origin;
            set => this._origin = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _effect = string.Empty;
        public string Effect
        {
            get => _effect;
            set => this._effect = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _x = string.Empty;
        public string X
        {
            get => _x;
            set => this._x = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _y = string.Empty;
        public string Y
        {
            get => _y;
            set => this._y = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _width = string.Empty;
        public string Width
        {
            get => _width;
            set => this._width = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _height = string.Empty;
        public string Height
        {
            get => _height;
            set => this._height = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _alpha = string.Empty;
        public string Alpha
        {
            get => _alpha;
            set => this._alpha = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _group = string.Empty;
        public string Group
        {
            get => _group;
            set => this._group = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set => this._text = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// implement of property
        /// </summary>
        /// <returns>value in property</returns>
        private string _metaData = string.Empty;
        public string MetaData
        {
            get => _metaData;
            set => this._metaData = string.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }

    [XmlRoot(ElementName = "document")]
    public class IMetaData
    {
        [XmlElement(ElementName = "slide")]
        public List<Item1> Value { get; set; }
    }

    public class Item1
    {
        [XmlAttribute("effect")]
        public string Effect { get; set; }

        [XmlElement(ElementName = "object")]
        public List<Item2> Value { get; set; }
    }

    public class Item2
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("target")]
        public string Target { get; set; }

        [XmlAttribute("origin")]
        public string Origin { get; set; }

        [XmlAttribute("effect")]
        public string Effect { get; set; }

        [XmlAttribute("x")]
        public string X { get; set; }

        [XmlAttribute("y")]
        public string Y { get; set; }

        [XmlAttribute("width")]
        public string Width { get; set; }

        [XmlAttribute("height")]
        public string Height { get; set; }

        [XmlAttribute("alpha")]
        public string Alpha { get; set; }

        [XmlAttribute("group")]
        public string Group { get; set; }

        [XmlElement(ElementName = "text")]
        public Text Text { get; set; }

        [XmlElement(ElementName = "metadata")]
        public MetaData MetaData { get; set; }
    }

    public class Text
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("height")]
        public string Height { get; set; }

        [XmlAttribute("width")]
        public string Width { get; set; }

        [XmlAttribute("weight")]
        public string Weight { get; set; }

        [XmlAttribute("align")]
        public string Align { get; set; }

        [XmlAttribute("italic")]
        public string Italic { get; set; }

        [XmlAttribute("interval")]
        public string Interval { get; set; }

        [XmlAttribute("lineinterval")]
        public string LineInterval { get; set; }

        [XmlAttribute("texttype")]
        public string TextType { get; set; }

        [XmlAttribute("fonttype")]
        public string FontType { get; set; }

        [XmlAttribute("blur")]
        public string Blur { get; set; }

        [XmlAttribute("shadow")]
        public string Shadow { get; set; }

        [XmlAttribute("shadowblur")]
        public string ShadowBlur { get; set; }

        [XmlAttribute("shadowx")]
        public string ShadowX { get; set; }

        [XmlAttribute("shadowy")]
        public string ShadowY { get; set; }

        [XmlAttribute("shadowalpha")]
        public string ShadowAlpha { get; set; }

        [XmlAttribute("face")]
        public string Face { get; set; }

        [XmlAttribute("edge")]
        public string Edge { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class MetaData
    {
        [XmlAttribute("format")]
        public string Format { get; set; }

        [XmlAttribute("repeat")]
        public string Repeat { get; set; }

        [XmlAttribute("frames")]
        public string Frames { get; set; }

        [XmlAttribute("ani")]
        public string Ani { get; set; }

        [XmlAttribute("anispd")]
        public string AniSpd { get; set; }

        [XmlAttribute("dis")]
        public string Dis { get; set; }

        [XmlAttribute("size")]
        public string Size { get; set; }

        [XmlAttribute("frequency")]
        public string Frequency { get; set; }

        [XmlAttribute("indexcount")]
        public string IndexCount { get; set; }

        [XmlAttribute("chartheight")]
        public string ChartHeight { get; set; }

        [XmlAttribute("startangle")]
        public string StartAngle { get; set; }

        [XmlAttribute("incliengle")]
        public string Incliengle { get; set; }

        [XmlAttribute("gradient")]
        public string Gradient { get; set; }

        [XmlAttribute("linecolor")]
        public string LineColor { get; set; }

        [XmlAttribute("linealpha")]
        public string LineAlpha { get; set; }

        [XmlAttribute("gradval")]
        public string Gradval { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
