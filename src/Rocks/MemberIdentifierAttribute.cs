﻿namespace Rocks;

/// <summary>
/// Specifies an identifier and a description
/// for a mocked member.
/// </summary>
/// <remarks>
/// This attribute is emitted into members on the mock type
/// so the verification call can get a meaningful stringified description
/// of the member that failed.
/// </remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
public sealed class MemberIdentifierAttribute
	 : Attribute
{
	/// <summary>
	/// Creates a new <see cref="MemberIdentifierAttribute"/> instance.
	/// </summary>
	/// <param name="value">The identifier.</param>
	/// <param name="description">The description.</param>
	public MemberIdentifierAttribute(int value, string description) =>
		(this.Value, this.Description) = (value, description);

	/// <summary>
	/// Gets the description.
	/// </summary>
	public string Description { get; }
	/// <summary>
	/// Gets the identifier.
	/// </summary>
	public int Value { get; }
}