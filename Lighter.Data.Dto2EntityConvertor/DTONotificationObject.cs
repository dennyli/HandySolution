using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Lighter.Data.Dto2Entity
{
    //public static class DTONotificationObjectEx
    //{
    //    public static U GetValue<T, U>(this T t, Expression<Func<T>> exp) where T : DTONotificationObject
    //    {
    //        var propertyName = PropertySupport.ExtractPropertyName(exp);
    //        return t.GetPropertyValue<U>(propertyName);
    //    }

    //    public static void SetValue<T, U>(this T t, Expression<Func<T>> exp, U value) where T : DTONotificationObject
    //    {
    //        var propertyName = PropertySupport.ExtractPropertyName(exp);
    //        t.SetPropertyValue<U>(propertyName, value);
    //    }
    //}

    [DataContract]
    [KnownType(typeof(IList<string>))]
    public class DTONotificationObject : NotificationObject, IDisposable
    {
        /// <summary>
        /// 属性值集合
        /// </summary>
        [DataMember]
        public IDictionary<string, object> _values = new Dictionary<string, object>();

        public DTONotificationObject()
        {
        }

        #region 根据属性名得到属性值
        public T GetPropertyValue<T>(
#if NET45
            [CallerMemberName]
#endif
            string propertyName
#if NET45
            = null
#endif
            )
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property name is invalid: " + propertyName);

            object _propertyValue;
            if(!_values.TryGetValue(propertyName,out _propertyValue))
            {
                _propertyValue = default(T);
                _values.Add(propertyName, _propertyValue);
            }
            return (T)_propertyValue;
        }
        #endregion

        public void SetPropertyValue<T>(T value, 
#if NET45
            [CallerMemberName]
#endif
            string propertyName
#if NET45
             = null
#endif
            )
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("invalid " + propertyName);

            if (!_values.ContainsKey(propertyName) || _values[propertyName] != (object)value)
            {
                _values[propertyName] = value;
                RaisePropertyChanged(propertyName);
            }
        }
 
        #region Dispose
        public void Dispose()
        {
            DoDispose();
        }

        ~DTONotificationObject()
        {
            DoDispose();
        }

        void DoDispose()
        {
            if (_values != null)
                _values.Clear();
        }
        #endregion
    }
}
