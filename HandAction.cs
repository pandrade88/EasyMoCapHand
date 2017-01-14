using UnityEngine;
using Windows.Kinect;
namespace EasyMoCapHand
{
    public class HandAction
    {
        bool hand = false, hOpen = true;
        Body[] b;
        Vector2 positionHand;
        int cont = 0;

        MotionCaptureResource mcp = new MotionCaptureResource();
        HandResources h;
        Quaternion orientation;

        // Use this for initialization
        public void Start(bool hand)
        {
            mcp.Start();
            h = new HandResources(hand);

        }

        // Update is called once per frame
        public void Update()
        {
            mcp.Update();
            b = mcp.getBodies();
            if (b != null)
            {
                for (int i = 0; i < b.Length; i++)
                {

                    if (b[i].IsTracked)
                    {
                        // Unity código this.transform.position = new Vector2
                        positionHand = h.getPositionXY(b[i]);
                        orientation = h.getOrientation(b[i]);
                        if (cont == 3)
                        {
                            if (h.getState(b[i]) == HandState.Closed || h.getState(b[i]) == HandState.Lasso || h.getState(b[i]) == HandState.Unknown)
                            {
                                hOpen = false;
                            }
                            else
                            {
                                hOpen = true;
                            }
                            cont = 0;
                        }
                        else
                            cont++;
                    }
                }
            }
        }

        public Vector2 getPosition()
        {
            return positionHand;
        }
        public void getPosition(ref Transform obj)
        {
            obj.localPosition = positionHand;
        }
        public float getPositionX()
        {
            return positionHand.x;
        }
        public float getPositionY()
        {
            return positionHand.y;
        }

        public bool openHand()
        {
            return hOpen;
        }

        public bool gotObject()
        {
            return !hOpen;
        }
        public Quaternion turn(Quaternion v)
        {
            return Quaternion.Slerp(v, orientation, 0.1f);
        }

        public void turnObject(ref Collider2D colisor)
        {
            colisor.transform.localRotation = Quaternion.Slerp(colisor.transform.localRotation, orientation, 0.1f);
        }

        public void moveObject(ref Collider2D colisor)
        {
            colisor.gameObject.transform.position = getPosition();
        }

        public void gotObject(bool move, bool turn, ref Collider2D colisor)
        {
            if(!hOpen)
            {
                if (move)
                    moveObject(ref colisor);
                if (turn)
                    turnObject(ref colisor);
            }
        }

        public void quit()
        {
            mcp.OnApplicationQuit();
        }

        
    }
}

