
namespace LabelPlus
{
    public class LabelItem
    {
        public float    X_percent;
        public float    Y_percent;
        public string   Text;

        public LabelItem(float x_percent, float y_percent, string text) {
            X_percent = x_percent;
            Y_percent = y_percent;
            Text = text;
        }
    }
}
