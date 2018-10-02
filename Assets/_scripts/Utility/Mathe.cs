public class Mathe
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="val"></param>
	/// <param name="oldMin"></param>
	/// <param name="oldMax"></param>
	/// <param name="newMin"></param>
	/// <param name="newMax"></param>
	/// <returns></returns>
	public static float Map(float val, float oldMin, float oldMax, float newMin, float newMax)
	{
		float oldRange = oldMax - oldMin;
		float newRange = newMax - newMin;
		float newVal = (((val - oldMin) / oldRange) * newRange) + newMin;
		return newVal;
	}
}
