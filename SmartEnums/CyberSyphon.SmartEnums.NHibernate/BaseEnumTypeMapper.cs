using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace CyberSyphon.SmartEnums.NHibernate;

// ReSharper disable once ClassNeverInstantiated.Global
public class BaseEnumTypeMapper<TType> : IUserType where TType : SmartEnum<TType>
{
    /// <inheritdoc />
    public bool IsMutable => false;

    /// <inheritdoc />
    public Type ReturnedType => typeof(TType);

    /// <inheritdoc />
    public SqlType[] SqlTypes => new SqlType[] { new StringSqlType() };

    /// <inheritdoc />
    public object Assemble(object cached, object owner)
    {
        return cached;
    }

    /// <inheritdoc />
    public object DeepCopy(object value)
    {
        return value;
    }

    /// <inheritdoc />
    public object Disassemble(object value)
    {
        return value;
    }

    /// <inheritdoc />
    public new bool Equals(object x, object y)
    {
        return object.Equals(x, y);
    }

    /// <inheritdoc />
    public int GetHashCode(object? x)
    {
        return x != null ? x.GetHashCode() : 0;
    }

    /// <inheritdoc />
    public object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
    {
        var value = NHibernateUtil.String.NullSafeGet(rs, names[0], session) as string;

        return SmartEnum<TType>.FindByName(value);
    }

    /// <inheritdoc />
    public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        var name = ((SmartEnum<TType>) value)?.Name;
        NHibernateUtil.String.NullSafeSet(cmd, name, index, session);
    }

    /// <inheritdoc />
    public object Replace(object original, object target, object owner)
    {
        return original;
    }
}