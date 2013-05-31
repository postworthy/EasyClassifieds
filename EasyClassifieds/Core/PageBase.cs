using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Objects;
using EasyClassifieds.Model;
using EasyClassifieds.Model.Base;
using System.Data;
using System.Reflection;
using System.Web.Security;

namespace EasyClassifieds.Core
{
    public class PageBase<ENTITY, CONTEXT> : System.Web.UI.Page
        where ENTITY : EntityBase, new()
        where CONTEXT : EasyContext, new()
    {
        private List<Expression<Func<bool>>> FieldMappings { get; set; }

        private Dictionary<Expression<Func<bool>>, Func<object, object>[]> Converters { get; set; }

        public MembershipUser LoggedInUser { get { return Membership.GetUser(); } }

        protected new virtual long ID 
        {
            get
            {
                 long id = 0;
                 long.TryParse(Request.QueryString["id"], out id);
                 return id;
            }
        }

        private ENTITY mEntity = null;
        public ENTITY Entity
        {
            get
            {
                if (mEntity == null)
                {
                   
                    if (ID > 0)
                    {
                        mEntity = EasyContext.Set<ENTITY>().Find(ID);
                    }
                    else mEntity = new ENTITY();
                }
                return mEntity;
            }
        }

        private CONTEXT mContext = null;
        public CONTEXT EasyContext 
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new CONTEXT();
                }
                return mContext;
            }
        }

        public PageBase()
            : base()
        {
            FieldMappings = new List<Expression<Func<bool>>>();
            Converters = new Dictionary<Expression<Func<bool>>, Func<object, object>[]>();
        }

        protected bool IsImage(string p)
        {
            p = p.ToLower();
            return p.Contains("png") || p.Contains("gif") || p.Contains("jpg") || p.Contains("jpeg");
        }

        public void AddFieldMappings(Expression<Func<bool>> mapping, Func<object, object> save = null, Func<object, object> load = null)
        {
            FieldMappings.Add(mapping);
            Converters.Add(mapping, new Func<object, object>[] {save, load});
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            if (!IsPostBack && FieldMappings.Count() > 0) LoadFromMappings(true);
            base.OnLoadComplete(e);
        }

        protected virtual void Update()
        {
            this.Validate();
            if (this.IsValid)
            {
                bool shouldNavigate = (Entity.ID == 0);

                if (FieldMappings.Count() > 0) LoadFromMappings(false);


                EasyContext.Entry<ENTITY>(Entity).State = (Entity.ID == 0) ? EntityState.Added : EntityState.Modified;
                EasyContext.SaveChanges();

                if (shouldNavigate)
                {
                    Response.Redirect(Request.Url.ToString().Split('?').First() + "?id=" + Entity.ID);
                }
            }
        }

        protected void LoadFromMappings(bool LoadFields)
        {
            /*
             * The code in this method walks the expression tree and 
             * maps either the value in the entity into the defined 
             * control property or pulls the value from the controls
             * property into the entity.
             * 
             * This allows us to define the mappings between the entity 
             * and the controls so that we can create a generic means of 
             * loading and extracting data from the form.
             * 
             */
            if (FieldMappings.Count() == 0) throw new Exception("No FieldMappings Defined!");

            foreach (var mapf in FieldMappings)
            {
                var map = mapf.Body as BinaryExpression;
                if (map != null && map.NodeType == ExpressionType.Equal)
                {
                    Expression left = null;
                    Expression right = null;


                    #region We can deal with ToString() and also can handle ToString() extension methods
                    if (map.Left.NodeType == ExpressionType.Call && ((MethodCallExpression)map.Left).Method.Name == "ToString")
                        left = (!((MethodCallExpression)map.Left).Method.IsStatic) ? ((MethodCallExpression)map.Left).Object : ((MethodCallExpression)map.Left).Arguments[0];
                    else
                        left = map.Left;

                    if (map.Right.NodeType == ExpressionType.Call && ((MethodCallExpression)map.Right).Method.Name == "ToString")
                        right = (!((MethodCallExpression)map.Right).Method.IsStatic) ? ((MethodCallExpression)map.Right).Object : ((MethodCallExpression)map.Right).Arguments[0];
                    else
                        right = map.Right;
                    #endregion

                    if (left != null && left.NodeType == ExpressionType.MemberAccess && ((MemberExpression)left).Member.DeclaringType.IsSubclassOf(typeof(EntityBase)))
                    {
                        if (right.NodeType == ExpressionType.MemberAccess)
                        {
                            if (LoadFields)
                                RecursiveMemberAccess(((MemberExpression)right), GetValue(map.Left, right.Type, Converters[mapf][1]));  //Field <--- Entity
                            else
                                RecursiveMemberAccess(((MemberExpression)left), GetValue(map.Right, left.Type, Converters[mapf][0])); //Field ---> Entity
                        }
                        else throw new Exception("Feild Mappings must map to a property on a derivative of the System.Web.UI.Control class!");
                    }
                    else if (right != null && right.NodeType == ExpressionType.MemberAccess && ((MemberExpression)right).Member.DeclaringType.IsSubclassOf(typeof(EntityBase)))
                    {
                        if (left.NodeType == ExpressionType.MemberAccess)
                        {
                            if (LoadFields)
                                RecursiveMemberAccess(((MemberExpression)left), GetValue(map.Right, left.Type, Converters[mapf][1])); //Field <--- Entity
                            else
                                RecursiveMemberAccess(((MemberExpression)right), GetValue(map.Left, right.Type, Converters[mapf][0])); //Field ---> Entity
                        }
                        else throw new Exception("Feild Mappings must map to a property on a derivative of the System.Web.UI.Control class!");
                    }
                    else throw new Exception("Feild Mappings must map to a property on a derivative of the EntityBase class!");
                }
                else throw new Exception("Only == is allowed in feild mappings!");
            }
        }

        private object GetValue(Expression exp, Type type, Func<object, object> converter = null)
        {
            LambdaExpression lambda = Expression.Lambda(typeof(Func<object>), Expression.Convert(exp, typeof(object)));
            object result = ((Func<object>)lambda.Compile())();
            if (result != null && !string.IsNullOrEmpty(result.ToString()) && result.ToString() != "0")
            {
                if (converter != null)
                    return converter(result);
                else
                    return Convert.ChangeType(result, type);
            }
            else
                return null;
        }

        private object RecursiveMemberAccess(MemberExpression prop, object value = null)
        {
            /*
             * This method actualy walks the property to the farthest 
             * node and gets or sets the value.
             */
            if (prop.Expression.NodeType == ExpressionType.Constant)
            {
                if (prop.Member is PropertyInfo)
                    return ((PropertyInfo)prop.Member).GetValue(((ConstantExpression)prop.Expression).Value, null);
                else
                    return ((FieldInfo)prop.Member).GetValue(((ConstantExpression)prop.Expression).Value);
            }
            else if (prop.Expression.NodeType == ExpressionType.MemberAccess)
            {
                value = (value != null ? Convert.ChangeType(value, prop.Type) : prop.Type.GetDefault());

                //Here we attempt to set the value and if need be we convert the type...or try to at least
                ((PropertyInfo)prop.Member).SetValue(RecursiveMemberAccess(prop.Expression as MemberExpression), value, null);

                return value;
            }
            else
                throw new Exception(String.Format("The expression type {0} is not supported to obtain a value.", prop.NodeType));

        }
    }
}