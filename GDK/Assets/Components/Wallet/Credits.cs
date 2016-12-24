using UnityEngine;

/// <summary>
/// Credits type so we don't have to use 'int' everywhere when refering to credits.
/// </summary>
public class Credits
{
	/// <summary>
	/// The credits integer value.
	/// </summary>
	private int value;

	/// <summary>
	/// Initializes a new instance of the <see cref="Credits"/> class.
	/// </summary>
	/// <param name="value">The credits integer value.</param>
	public Credits (int value)
	{
		this.value = value;
	}

	public static implicit operator Credits (int value)
	{
		return new Credits (value);
	}

	public static implicit operator int (Credits credits)
	{
		return credits.value;
	}
}