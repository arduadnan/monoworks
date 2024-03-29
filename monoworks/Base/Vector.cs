//   Vector.cs - MonoWorks Project
//
//    Copyright Andy Selvig 2008
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published 
//    by the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Lesser General Public License for more details.
//
//    axis[1]ou should have received a copy of the GNU Lesser General Public 
//    License along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;


namespace MonoWorks.Base
{
	
	
	/// <summary>
	/// The Vector class represents a vector in 3D space. 
	/// </summary>
	public class Vector : IStringParsable
	{
		/// <summary>
		/// Default initializer.
		/// All coordinates are set to zero.
		/// </summary>
		public Vector()
		{
			_val = new double[]{0.0, 0.0, 0.0};
		}
		
		
		/// <summary>
		/// Initialization constructor.
		/// </summary>
		/// <param name="x"> A <see cref="System.Double"/> representing x. </param>
		/// <param name="y"> A <see cref="System.Double"/> representing y. </param>
		/// <param name="z"> A <see cref="System.Double"/> representing z. </param>
		public Vector(double x, double y, double z)
		{
			_val = new double[]{x, y, z};
		}
		
		
#region Coordinates
		
		/// <summary>
		/// Actual value of the coordinates.
		/// </summary>
		protected double [] _val;
		
		/// <summary>
		/// Access the coordinate (x,y,z) values by index.
		/// </summary>
		public double this[int index]
		{
			get
			{
				// ensure the index has appropriate range
				if (index<0 || index>2)
					throw new Exception("index is out of bounds!");	
				return _val[index];
			}
			set
			{
				// ensure the index has appropriate range
				if (index<0 || index>2)
					throw new Exception("index is out of bounds!");	
				_val[index] = value;
			}
		}

		/// <summary>
		/// The value in the x dimension.
		/// </summary>
		public double X
		{
			get { return _val[0]; }
			set { _val[0] = value; }
		}

		/// <summary>
		/// The value in the y dimension.
		/// </summary>
		public double Y
		{
			get { return _val[1]; }
			set { _val[1] = value; }
		}

		/// <summary>
		/// The value in the z dimension.
		/// </summary>
		public double Z
		{
			get { return _val[2]; }
			set { _val[2] = value; }
		}

		/// <value>
		/// The index of the dimension with the largest value.
		/// </value>
		public int LargestDimension
		{
			get
			{
				double max = Math.Max(Math.Max(_val[0], _val[1]), _val[2]);
				return Array.IndexOf(_val, max);
			}
		}
		
#endregion
		

#region Vector Math
		
		/// <value>
		/// The magnitude of the vector.
		/// </value>
		public double Magnitude
		{
			get {return Math.Sqrt(Math.Pow(_val[0], 2) + Math.Pow(_val[1], 2) + Math.Pow(_val[2], 2));}
		}
		
		
		/// <summary>
		/// Normalizes the vector so the magnitude is 1.0 and return the result.
		/// </summary>
		public Vector Normalize()
		{
			double mag = Magnitude;
			return new Vector(_val[0] / mag, _val[1] / mag, _val[2] / mag);
		}
				
		
		/// <summary>
		/// Performs a cross product on two vectors.
		/// </summary>
		/// <param name="rhs"> The second <see cref="Vector"/>. </param>
		/// <returns> The resulting <see cref="Vector"/>. </returns>
		public Vector Cross(Vector rhs)
		{
			Vector res = new Vector();
			res[0] = _val[1]*rhs[2] - _val[2]*rhs[1];
			res[1] = _val[2]*rhs[0] - _val[0]*rhs[2];
			res[2] = _val[0]*rhs[1] - _val[1]*rhs[0];
			return res;
		}
		
		/// <summary>
		/// Performs a dot product on two vectors.
		/// </summary>
		/// <param name="rhs"> The second <see cref="Vector"/>. </param>
		/// <returns> The resulting value. </returns>
		public double Dot(Vector rhs)
		{
			return _val[0]*rhs[0] + _val[1]*rhs[1] + _val[2]*rhs[2];
		}
		
#endregion
				
		
#region Operator Overloading
		
		/// <summary>
		/// Adds the elements of each vector.
		/// </summary>
		/// <param name="lhs"> The left hand operand. </param>
		/// <param name="rhs"> The right hand operand. </param>
		/// <returns> The resulting <see cref="Vector"/>. </returns>
		public static Vector operator +(Vector lhs, Vector rhs)
		{
			return new Vector(lhs[0] + rhs[0], lhs[1] + rhs[1], lhs[2] + rhs[2]);
		}

		/// <summary>
		/// Adds the scalar to the elements of the vector.
		/// </summary>
		public static Vector operator +(Vector lhs, double rhs)
		{
			return new Vector(lhs[0] + rhs, lhs[1] + rhs, lhs[2] + rhs);
		}
		
		/// <summary>
		/// Subtracts the elements of each vector.
		/// </summary>
		/// <param name="lhs"> The left hand operand. </param>
		/// <param name="rhs"> The right hand operand. </param>
		/// <returns> The resulting <see cref="Vector"/>. </returns>
		public static Vector operator-(Vector lhs, Vector rhs)
		{
			return new Vector(lhs[0]-rhs[0], lhs[1]-rhs[1], lhs[2]-rhs[2]);
		}
		
		/// <summary>
		/// Multiplies each element in vector by factor and returns the result.
		/// </summary>
		/// <param name="vector"> The left hand operand. </param>
		/// <param name="factor"> The right hand operand. </param>
		/// <returns> The resulting <see cref="Vector"/>. </returns>
		public static Vector operator*(Vector vector, double factor)
		{
			return new Vector(vector[0]*factor, vector[1]*factor, vector[2]*factor);
		}
		
		/// <summary>
		/// Divides each element in vector by factor and returns the result.
		/// </summary>
		/// <param name="vector"> The left hand operand. </param>
		/// <param name="factor"> The right hand operand. </param>
		/// <returns> The resulting <see cref="Vector"/>. </returns>
		public static Vector operator/(Vector vector, double factor)
		{
			return new Vector(vector[0]/factor, vector[1]/factor, vector[2]/factor);
		}

		/// <summary>
		/// Element-by-element multiplication.
		/// </summary>
		/// <param name="lhs"> The left hand operand. </param>
		/// <param name="rhs"> The right hand operand. </param>
		/// <returns> The resulting <see cref="Vector"/>. </returns>
		public static Vector operator*(Vector lhs, Vector rhs)
		{
			return new Vector(lhs[0] * rhs[0], lhs[1] * rhs[1], lhs[2] * rhs[2]);
		}

		/// <summary>
		/// Element-by-element division.
		/// </summary>
		/// <param name="lhs"> The left hand operand. </param>
		/// <param name="rhs"> The right hand operand. </param>
		/// <returns> The resulting <see cref="Vector"/>. </returns>
		public static Vector operator/(Vector lhs, Vector rhs)
		{
			return new Vector(lhs[0] / rhs[0], lhs[1] / rhs[1], lhs[2] / rhs[2]);
		}

		/// <summary>
		/// Inverts the vector.
		/// </summary>
		public Vector Invert()
		{
			return new Vector(-X, -Y, -Z);
		}
		
#endregion
				
		
#region Rotation
		
		/// <summary>
		/// Rotates the vector about the given axis with the given angle.
		/// </summary>
		/// <param name="axis"> Axis of rotation. </param>
		/// <param name="angle"> Angle of rotation. </param>
		public Vector Rotate(Vector axis, Angle angle)
		{
			// quaternion method
			Quaternion R = new Quaternion();
			R.Scalar = (angle/2.0).Cos();
			R.Vector = axis.Normalize() * (angle/2.0).Sin();
			Quaternion v = new Quaternion(0.0, this);
			Quaternion res = R * v * R.T();
			return res.Vector;
		}
		
#endregion
		

#region Comparison
		
		/// <summary>
		/// Keeps the smallest value in each dimension.
		/// </summary>
		/// <param name="other"> Another <see cref="Vector"/>. </param>
		public void KeepMinima(Vector other)
		{
			for (int i=0; i<3; i++)
				_val[i] = Math.Min( _val[i], other[i]);
		}
		
		/// <summary>
		/// Keeps the smallest value in each dimension.
		/// </summary>
		public void KeepMinima(double x, double y, double z)
		{
			_val[0] = Math.Min(_val[0], x);
			_val[1] = Math.Min(_val[1], y);
			_val[2] = Math.Min(_val[1], z);
		}
		
		/// <summary>
		/// Keeps the largest value in each dimension.
		/// </summary>
		/// <param name="other"> Another <see cref="Vector"/>. </param>
		public void KeepMaxima(Vector other)
		{
			for (int i=0; i<3; i++)
				_val[i] = Math.Max( _val[i], other[i]);
		}

		/// <summary>
		/// Keeps the largest value in each dimension.
		/// </summary>
		public void KeepMaxima(double x, double y, double z)
		{
			_val[0] = Math.Max(_val[0], x);
			_val[1] = Math.Max(_val[1], y);
			_val[2] = Math.Max(_val[1], z);
		}
		
#endregion
		
		
		/// <summary>
		/// Produces a vector of the copy.
		/// </summary>
		public Vector Copy()
		{
			return new Vector(_val[0], _val[1], _val[2]);
		}
		
		/// <summary>
		/// Returns a string displaying the coordinate values.
		/// </summary>
		public override string ToString()
		{
			return String.Format("[{0:g4},{1:g4},{2:g4}]", _val[0], _val[1], _val[2]);
		}
		
		/// <summary>
		/// Compares two vectors to see if they are the same.
		/// </summary>
		/// <param name="other"> Something to compare to.</param>
		/// <returns> True if they're the same (have the same x, y, and z). </returns>
		public override bool Equals(object other)
		{
			if (other == null)
				return false;
			if (!(other is Vector))
				throw new Exception("Only compare Vectors to other Vectors.");
			double[] otherVal = (other as Vector)._val;
			return _val[0]==otherVal[0] && _val[1]==otherVal[1] && _val[2]==otherVal[2]; 
		}

		/// <summary>
		/// Equality operator overload for vectors.
		/// </summary>
		public static bool operator ==(Vector v1, Vector v2)
		{
			if ((object)v1 == null && (object)v2 == null) // nulls are equal
				return true;
			if ((object)v1 == null)
				return false;
			return v1.Equals(v2);
		}

		/// <summary>
		/// Inequality operator overload for vectors.
		/// </summary>
		public static bool operator !=(Vector v1, Vector v2)
		{
			if ((object)v1 == null)
				return false;
			return !v1.Equals(v2);
		}

		/// <summary>
		/// For some reason I have to implement this because I override Equals().
		/// </summary>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Parses a string defining a vector with the form [x,y,z].
		/// </summary>
		public void Parse(string valString)
		{
			if (valString.StartsWith("["))
				valString = valString.Substring(1);
			if (valString.EndsWith("]"))
				valString = valString.Substring(0, valString.Length - 1);
			var comps = valString.Split(',');
			if (comps.Length != 3)
				throw new Exception("Vector literals should have 3 comma-separated components");
			for (int i = 0; i < 3; i++)
			{
				_val[i] = double.Parse(comps[i]);
			}
		}
		
	}
}
