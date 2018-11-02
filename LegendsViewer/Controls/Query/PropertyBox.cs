﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LegendsViewer.Controls.Query
{
    public class PropertyBox : ComboBox
    {
        public PropertyBox Child;
        private PropertyBox _parentProperty;
        public bool ListPropertiesOnly;
        private Type _parentType;
        public Type ParentType
        {
            get { return _parentType; }
            set
            {
                _parentType = value;
                Items.Clear();
                Items.Add("");
                List<SearchProperty> availableProperties;
                if (ListPropertiesOnly)
                {
                    availableProperties = SearchProperty.GetProperties(value).Where(property => property.IsSelectable).ToList();// property.SubProperties.Count > 1).ToList();
                }
                else
                {
                    availableProperties = SearchProperty.GetProperties(value);
                }

                foreach (SearchProperty property in availableProperties)
                {
                    if (property.Name == "Value" && _parentProperty != null && _parentProperty.SelectedProperty != null)
                    {
                        string description = _parentProperty.SelectedProperty.Description;
                        if (description == "Populations" || description == "Deaths" || description == "Attackers" || description == "Defenders")
                        {
                            property.Description = "Race";
                        }
                        else if (description == "Associated Spheres")
                        {
                            property.Description = "Sphere";
                        }
                    }
                }
                Items.AddRange(availableProperties.ToArray());
            }
        }
        public SearchProperty SelectedProperty;

        public PropertyBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            DropDownWidth = 175;
            Width = 50;
            MinimumSize = new Size(50, 0);
            //ForeColor = Color.Maroon;
            //Visible = true;
            //Height = 10;
            //Width = 30;
        }

        public Type GetLowestPropertyType()
        {
            if (SelectedProperty != null && Child == null)
            {
                return SelectedProperty.Type;
            }

            if (SelectedProperty == null)
            {
                return ParentType;
            }

            return Child.GetLowestPropertyType();
        }

        public SearchProperty GetLowestProperty()
        {
            if (Child == null)
            {
                return SelectedProperty;
            }

            SearchProperty property = Child.GetLowestProperty();
            if (property == null)
            {
                return SelectedProperty;
            }

            return property;
        }

        public List<PropertyInfo> GetSelectedProperties(List<PropertyInfo> properties = null)
        {
            if (properties == null)
            {
                properties = new List<PropertyInfo>();
            }

            if (SelectedProperty == null)
            {
                return properties;
            }

            properties.Add(ParentType.GetProperty(SelectedProperty.Name));
            return properties;
        }

        public void Remove()
        {
            if (Child != null)
            {
                PropertyBox temp = Child;
                Child = null;
                temp.Remove();
            }
            Parent.Controls.Remove(this);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            if (Child != null)
            {
                Child.Location = new Point(Right + 3, Top);
            }
        }

        public int GetRightSide()
        {
            if (Child == null)
            {
                return Right;
            }

            return Child.GetRightSide();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            Graphics g = CreateGraphics();
            Width = (int)g.MeasureString(Text, Font).Width + 20;
            g.Dispose();

            if (!SelectedItem.Equals(""))
            {
                SelectedProperty = SelectedItem as SearchProperty;
            }
            else
            {
                SelectedProperty = null;
            }

            if (Child != null)
            {
                PropertyBox temp = Child;
                Child = null;
                temp.Remove();
            }

            //if listproperties only and subproperties contains other lists or !list
            if (!ListPropertiesOnly && SelectedProperty != null && (!ListPropertiesOnly && SelectedProperty.SubProperties.Any() || ListPropertiesOnly && SelectedProperty.SubProperties.Count(property => property.SubProperties.Count > 0) > 0))
            {
                Child = new PropertyBox
                {
                    _parentProperty = this,
                    ParentType = (SelectedItem as SearchProperty).Type,
                    ListPropertiesOnly = ListPropertiesOnly,
                    Location = new Point(Right + 0, Top)
                };
                Parent.Controls.Add(Child);
            }
            if (Parent.GetType() == typeof(CriteriaLine))
            {
                (Parent as CriteriaLine).GetComparers();
                (Parent as CriteriaLine).ValueSelect.ResetText();
                (Parent as CriteriaLine).GetValueOptions();
                (Parent as CriteriaLine).ResizeSelf();
            }

            base.OnSelectedIndexChanged(e);
        }

        public void SetupSearchProperties(SearchInfo criteria)
        {
            criteria.PropertyName = SelectedProperty.Name;
            if (Child != null && Child.SelectedProperty != null)
            {
                criteria.Next = SearchInfo.Make(Child.ParentType);
                criteria.Next.Operator = QueryOperator.Or;
                criteria.Next.Previous = criteria;
                Child.SetupSearchProperties(criteria.Next);
            }
        }

        public bool ContainsList()
        {
            if (SelectedProperty == null)
            {
                return false;
            }

            if (SelectedProperty.Type.IsGenericType)
            {
                return true;
            }

            if (Child != null)
            {
                return Child.ContainsList();
            }

            return false;
        }

        public bool ContainsListLast()
        {
            if (SelectedProperty.Type.IsGenericType && Child != null && Child.SelectedProperty != null)
            {
                return false;
            }

            if (SelectedProperty.Type.IsGenericType && Child != null && Child.SelectedProperty == null)
            {
                return true;
            }

            if (SelectedProperty.Type.IsGenericType && Child == null)
            {
                return true;
            }

            if (Child != null)
            {
                return Child.ContainsListLast();
            }

            return false;
        }
    }
}
