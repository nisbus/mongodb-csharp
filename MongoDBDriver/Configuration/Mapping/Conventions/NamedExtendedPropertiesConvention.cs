﻿using System;
using System.Reflection;

namespace MongoDB.Driver.Configuration.Mapping.Conventions
{
    public class NamedExtendedPropertiesConvention : IExtendedPropertiesConvention
    {
        private readonly BindingFlags _bindingFlags;
        private readonly string _memberName;
        private readonly MemberTypes _memberTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedExtendedPropertiesConvention"/> class.
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        public NamedExtendedPropertiesConvention(string memberName)
            : this(memberName, MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedExtendedPropertiesConvention"/> class.
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="memberTypes">The member types.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        public NamedExtendedPropertiesConvention(string memberName, MemberTypes memberTypes, BindingFlags bindingFlags)
        {
            _bindingFlags = bindingFlags;
            _memberName = memberName;
            _memberTypes = memberTypes;
        }

        /// <summary>
        /// Gets the member representing extended properties if one exists.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public MemberInfo GetExtendedPropertiesMember(Type type)
        {
            var foundMembers = type.FindMembers(_memberTypes, _bindingFlags, IsMemberWithName, null);
            if (foundMembers.Length == 0)
                return null;
            if (foundMembers.Length == 1)
                return foundMembers[0];

            throw new Exception("Too many members found matching Id criteria.");
        }

        /// <summary>
        /// Determines whether [is member with name] [the specified member info].
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// 	<c>true</c> if [is member with name] [the specified member info]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMemberWithName(MemberInfo memberInfo, object criteria)
        {
            //TODO: check type is IDictionary<string, object> or Document.
            return memberInfo.Name == _memberName;
        }
    }
}