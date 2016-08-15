using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.Util;
using Android.Widget;
using Java.Lang;
using Exception = System.Exception;
using Math = System.Math;

namespace IntranetMobile.Droid.Views.Controls
{
    public class CircleImageView : ImageView
    {
        private static readonly ScaleType SCALE_TYPE = ScaleType.CenterCrop;

        private static readonly Bitmap.Config BITMAP_CONFIG = Bitmap.Config.Argb8888;
        private static readonly int COLORDRAWABLE_DIMENSION = 2;

        private static readonly int DEFAULT_BORDER_WIDTH = 0;
        private static readonly int DEFAULT_BORDER_COLOR = Color.Black;
        private static readonly int DEFAULT_FILL_COLOR = Color.Transparent;
        private static readonly bool DEFAULT_BORDER_OVERLAY = false;
        private readonly Paint mBitmapPaint = new Paint();
        private readonly Paint mBorderPaint = new Paint();
        private readonly RectF mBorderRect = new RectF();

        private readonly RectF mDrawableRect = new RectF();
        private readonly int mFillColor = DEFAULT_FILL_COLOR;
        private readonly Paint mFillPaint = new Paint();

        private readonly Matrix mShaderMatrix = new Matrix();

        private Bitmap mBitmap;
        private int mBitmapHeight;
        private BitmapShader mBitmapShader;
        private int mBitmapWidth;

        private int mBorderColor = DEFAULT_BORDER_COLOR;
        private bool mBorderOverlay;
        private float mBorderRadius;
        private int mBorderWidth = DEFAULT_BORDER_WIDTH;

        private ColorFilter mColorFilter;
        private bool mDisableCircularTransformation;

        private float mDrawableRadius;

        private bool mReady;
        private bool mSetupPending;

        public CircleImageView(Context context) : base(context)
        {
            init();
        }

        public CircleImageView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public CircleImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.CircleImageView, defStyle, 0);

            mBorderWidth = a.GetDimensionPixelSize(Resource.Styleable.CircleImageView_civ_border_width,
                DEFAULT_BORDER_WIDTH);
            mBorderColor = a.GetColor(Resource.Styleable.CircleImageView_civ_border_color, DEFAULT_BORDER_COLOR);
            mBorderOverlay = a.GetBoolean(Resource.Styleable.CircleImageView_civ_border_overlay, DEFAULT_BORDER_OVERLAY);
            mFillColor = a.GetColor(Resource.Styleable.CircleImageView_civ_fill_color, DEFAULT_FILL_COLOR);

            a.Recycle();

            init();
        }

        public override ColorFilter ColorFilter
        {
            get { return mColorFilter; }
        }

        private void init()
        {
            base.SetScaleType(SCALE_TYPE);
            mReady = true;

            if (mSetupPending)
            {
                Setup();
                mSetupPending = false;
            }
        }

        public override ScaleType GetScaleType()
        {
            return SCALE_TYPE;
        }


        public override void SetScaleType(ScaleType scaleType)
        {
            if (scaleType != SCALE_TYPE)
            {
                throw new IllegalArgumentException(string.Format("ScaleType %s not supported.", scaleType));
            }
        }

        public override void SetAdjustViewBounds(bool adjustViewBounds)
        {
            if (adjustViewBounds)
            {
                throw new IllegalArgumentException("adjustViewBounds not supported.");
            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (mDisableCircularTransformation)
            {
                base.OnDraw(canvas);
                return;
            }

            if (mBitmap == null)
            {
                return;
            }

            if (mFillColor != Color.Transparent)
            {
                canvas.DrawCircle(mDrawableRect.CenterX(), mDrawableRect.CenterY(), mDrawableRadius, mFillPaint);
            }
            canvas.DrawCircle(mDrawableRect.CenterX(), mDrawableRect.CenterY(), mDrawableRadius, mBitmapPaint);
            if (mBorderWidth > 0)
            {
                canvas.DrawCircle(mBorderRect.CenterX(), mBorderRect.CenterY(), mBorderRadius, mBorderPaint);
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            Setup();
        }

        public override void SetPadding(int left, int top, int right, int bottom)
        {
            base.SetPadding(left, top, right, bottom);
            Setup();
        }

        public override void SetPaddingRelative(int start, int top, int end, int bottom)
        {
            base.SetPaddingRelative(start, top, end, bottom);
            Setup();
        }

        public int GetBorderColor()
        {
            return mBorderColor;
        }

        public void SetBorderColor(int borderColor)
        {
            if (borderColor == mBorderColor)
            {
                return;
            }

            mBorderColor = borderColor;
            mBorderPaint.Color = new Color(mBorderColor);
            Invalidate();
        }


        public int GetBorderWidth()
        {
            return mBorderWidth;
        }

        public void SetBorderWidth(int borderWidth)
        {
            if (borderWidth == mBorderWidth)
            {
                return;
            }

            mBorderWidth = borderWidth;
            Setup();
        }

        public bool IsBorderOverlay()
        {
            return mBorderOverlay;
        }

        public void SetBorderOverlay(bool borderOverlay)
        {
            if (borderOverlay == mBorderOverlay)
            {
                return;
            }

            mBorderOverlay = borderOverlay;
            Setup();
        }

        public bool IsDisableCircularTransformation()
        {
            return mDisableCircularTransformation;
        }

        public void SetDisableCircularTransformation(bool disableCircularTransformation)
        {
            if (mDisableCircularTransformation == disableCircularTransformation)
            {
                return;
            }

            mDisableCircularTransformation = disableCircularTransformation;
            InitializeBitmap();
        }

        public override void SetImageBitmap(Bitmap bm)
        {
            base.SetImageBitmap(bm);
            InitializeBitmap();
        }

        public override void SetImageDrawable(Drawable drawable)
        {
            base.SetImageDrawable(drawable);
            InitializeBitmap();
        }

        public override void SetImageResource(int resId)
        {
            base.SetImageResource(resId);
            InitializeBitmap();
        }

        public override void SetImageURI(Uri uri)
        {
            base.SetImageURI(uri);
            InitializeBitmap();
        }

        public override void SetColorFilter(ColorFilter cf)
        {
            if (cf == mColorFilter)
            {
                return;
            }

            mColorFilter = cf;
            ApplyColorFilter();
            Invalidate();
        }

        private void ApplyColorFilter()
        {
            if (mBitmapPaint != null)
                mBitmapPaint.SetColorFilter(mColorFilter);
        }

        private Bitmap GetBitmapFromDrawable(Drawable drawable)
        {
            if (drawable == null)
            {
                return null;
            }

            var bitmapDrawable = drawable as BitmapDrawable;
            if (bitmapDrawable != null)
            {
                return bitmapDrawable.Bitmap;
            }

            try
            {
                Bitmap bitmap;

                if (drawable is ColorDrawable)
                {
                    bitmap = Bitmap.CreateBitmap(COLORDRAWABLE_DIMENSION, COLORDRAWABLE_DIMENSION, BITMAP_CONFIG);
                }
                else
                {
                    bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight, BITMAP_CONFIG);
                }

                var canvas = new Canvas(bitmap);
                drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
                drawable.Draw(canvas);
                return bitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void InitializeBitmap()
        {
            mBitmap = mDisableCircularTransformation ? null : GetBitmapFromDrawable(Drawable);
            Setup();
        }

        private void Setup()
        {
            if (!mReady)
            {
                mSetupPending = true;
                return;
            }

            if (Width == 0 && Height == 0)
            {
                return;
            }

            if (mBitmap == null)
            {
                Invalidate();
                return;
            }

            mBitmapShader = new BitmapShader(mBitmap, Shader.TileMode.Clamp, Shader.TileMode.Clamp);

            mBitmapPaint.AntiAlias = true;
            mBitmapPaint.SetShader(mBitmapShader);

            mBorderPaint.SetStyle(Paint.Style.Stroke);
            mBorderPaint.AntiAlias = true;
            mBorderPaint.Color = new Color(mBorderColor);
            mBorderPaint.StrokeWidth = mBorderWidth;

            mFillPaint.SetStyle(Paint.Style.Fill);
            mFillPaint.AntiAlias = true;
            mFillPaint.Color = new Color(mFillColor);

            mBitmapHeight = mBitmap.Height;
            mBitmapWidth = mBitmap.Width;

            mBorderRect.Set(CalculateBounds());
            mBorderRadius = Math.Min((mBorderRect.Height() - mBorderWidth)/2.0f,
                (mBorderRect.Width() - mBorderWidth)/2.0f);

            mDrawableRect.Set(mBorderRect);
            if (!mBorderOverlay && mBorderWidth > 0)
            {
                mDrawableRect.Inset(mBorderWidth - 1.0f, mBorderWidth - 1.0f);
            }
            mDrawableRadius = Math.Min(mDrawableRect.Height()/2.0f, mDrawableRect.Width()/2.0f);

            ApplyColorFilter();
            UpdateShaderMatrix();
            Invalidate();
        }

        private RectF CalculateBounds()
        {
            var availableWidth = Width - PaddingEnd - PaddingRight;
            var availableHeight = Height - PaddingTop - PaddingBottom;

            var sideLength = Math.Min(availableWidth, availableHeight);

            var left = PaddingLeft + (availableWidth - sideLength)/2f;
            var top = PaddingTop + (availableHeight - sideLength)/2f;

            return new RectF(left, top, left + sideLength, top + sideLength);
        }

        private void UpdateShaderMatrix()
        {
            float scale;
            float dx = 0;
            float dy = 0;

            mShaderMatrix.Set(null);

            if (mBitmapWidth*mDrawableRect.Height() > mDrawableRect.Width()*mBitmapHeight)
            {
                scale = mDrawableRect.Height()/mBitmapHeight;
                dx = (mDrawableRect.Width() - mBitmapWidth*scale)*0.5f;
            }
            else
            {
                scale = mDrawableRect.Width()/mBitmapWidth;
                dy = (mDrawableRect.Height() - mBitmapHeight*scale)*0.5f;
            }

            mShaderMatrix.SetScale(scale, scale);
            mShaderMatrix.PostTranslate((int) (dx + 0.5f) + mDrawableRect.Left, (int) (dy + 0.5f) + mDrawableRect.Top);

            mBitmapShader.SetLocalMatrix(mShaderMatrix);
        }
    }
}