using System;

namespace Geometry {
	public class Vector3 {
		public double x { get; set; }
		public double y { get; set; }
		public double z { get; set; }
		public Vector3(double x, double y,double z){
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public override string ToString(){
			return $"{x} {y} {z}";
		}
	}
}