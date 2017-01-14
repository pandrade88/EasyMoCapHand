using Windows.Kinect;

namespace EasyMoCapHand
{
    public class MotionCaptureResource
    {

        private KinectSensor sensor;
        private BodyFrameReader reader;
        private Body[] data = null;
        public Body[] getBodies()
        {
            return data;
        }
        
        public void Start()
        {
            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
                reader = sensor.BodyFrameSource.OpenReader();

                if (!sensor.IsOpen)
                {
                    sensor.Open();
                }
            }
        }

        public void Update()
        {
            if (reader != null)
            {
                var frame = reader.AcquireLatestFrame();
                if (frame != null)
                {
                    if (data == null)
                    {
                        data = new Body[sensor.BodyFrameSource.BodyCount];
                    }

                    frame.GetAndRefreshBodyData(data);

                    frame.Dispose();
                    frame = null;
                }
            }
        }
        public void OnApplicationQuit() // Código Unity
        {
            if (reader != null)
            {
                reader.Dispose();
                reader = null;
            }

            if (sensor != null)
            {
                if (sensor.IsOpen)
                {
                    sensor.Close();
                }

                sensor = null;
            }
        }
    }

}