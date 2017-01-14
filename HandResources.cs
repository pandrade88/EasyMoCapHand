using UnityEngine;
using System.Collections;
using Windows.Kinect;
namespace EasyMoCapHand
{
    public class HandResources
    {
        
        Windows.Kinect.JointType hand;
       
        
        public HandResources (bool right)
        {
            if(right)
            {
                hand = Windows.Kinect.JointType.HandRight;
            }
            else
            {
                hand = Windows.Kinect.JointType.HandLeft;
            }
        }

        public Vector2 getPositionXY(Body bodyTracked)
        {
            return new Vector2(bodyTracked.Joints[hand].Position.X*10, bodyTracked.Joints[hand].Position.Y*10);
        }

        public HandState getState(Body bodyTracked)
        {
            return bodyTracked.HandRightState;
        }

        public Quaternion getOrientation(Body bodyTracked)
        {
            float ox, oy,oz,ow;
                    ox = bodyTracked.JointOrientations[Windows.Kinect.JointType.HandRight].Orientation.X;
                    oy = bodyTracked.JointOrientations[Windows.Kinect.JointType.HandRight].Orientation.Y;
                    oz = bodyTracked.JointOrientations[Windows.Kinect.JointType.HandRight].Orientation.Z;
                    ow = bodyTracked.JointOrientations[Windows.Kinect.JointType.HandRight].Orientation.W;

                    
                        return new Quaternion(ox, oy, oz*360 , ow*360);
           
        }
      
    }
}
