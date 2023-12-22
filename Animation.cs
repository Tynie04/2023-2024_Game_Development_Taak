using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
	class Animation
	{
		public AnimationFrame CurrentFrame { get; set; }
		private List<AnimationFrame> frames;
		private int counter;
		private double secondCounter = 0;

		public Animation()
		{
			frames = new List<AnimationFrame>();
		}

		public void AddFrame(AnimationFrame frame)
		{
			frames.Add(frame);
			CurrentFrame = frames[0];
		}

		public void Update(GameTime gameTime)
		{
			CurrentFrame = frames[counter];

			secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
			int fps = 10;

			if (secondCounter >= 1d / fps)
			{
				counter++;
				secondCounter = 0;
			}

			if (counter >= frames.Count)
			{
				counter = 0;
			}
		}


		public void GetFramesFromTextureProperties
			(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites, int lengte, int hoogte) // voor lengte en hoogte: denken zoals array
		{
			int widthOffFrame = width / numberOfWidthSprites;
			int heightOffFrame = height / numberOfHeightSprites;

			int useHeight = heightOffFrame * hoogte;

			for (int y = 0; y <= lengte; y += heightOffFrame)
			{
				for (int x = 0; x <= widthOffFrame * lengte; x += widthOffFrame)
				{
					frames.Add(new AnimationFrame(
						new Rectangle(x, useHeight, widthOffFrame, heightOffFrame)));
				}
			}
		}
	}
}
