using UnityEngine;

public class Credits
{
	private int value;

	public Credits(int value)
	{
		this.value = value;
	}
		
	public static implicit operator Credits(int value)
	{
		return new Credits(value);
	}
		
	public static implicit operator int(Credits credits)
	{
		return credits.value;
	}
}