
using System;

namespace Geometry{

	public class Vector2{
		public double x { get; set; }
		public double y { get; set; }
		public Vector2(double x, double y){
			this.x = x;
			this.y = y;
		}
		public override string ToString(){
			return $"{x} {y}";
		}
	}
}