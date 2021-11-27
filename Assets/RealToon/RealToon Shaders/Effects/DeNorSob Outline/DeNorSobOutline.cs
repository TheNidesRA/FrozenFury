using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DeNorSobOutline : ScriptableRendererFeature
{
    class DeNorSobOutlinePass : ScriptableRenderPass
    {

        private RenderTargetIdentifier source { get; set; }
        private RenderTargetHandle destination { get; set; }

        public Material DeNorSobOutlineMat = null;

        RenderTargetHandle temporaryColorTexture;

        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public DeNorSobOutlinePass(string profilerTag, RenderPassEvent renderPassEvent, Shader DeNorSobOutlineShader,
        float _OutlineWidth,
        float _DepthThreshold,
        float _NormalThreshold,
        float _NormalMin,
        float _NormalMax,

        bool _SobOutSel,
        float _SobelOutlineThreshold,
        float _WhiThres,
        float _BlaThres,

        Color _OutlineColor,
        float _OutlineColorIntensity,
        bool _ColOutMiSel,

        bool _OutOnSel,
        bool _MixDeNorSob)
        {

            this.renderPassEvent = renderPassEvent;

            DeNorSobOutlineMat = new Material(DeNorSobOutlineShader);
            DeNorSobOutlineMat.SetFloat("_OutlineWidth", _OutlineWidth);
            DeNorSobOutlineMat.SetFloat("_DepthThreshold", _DepthThreshold);

            DeNorSobOutlineMat.SetFloat("_NormalThreshold", _NormalThreshold);
            DeNorSobOutlineMat.SetFloat("_NormalMin", _NormalMin);
            DeNorSobOutlineMat.SetFloat("_NormalMax", _NormalMax);

            DeNorSobOutlineMat.SetFloat("_SobOutSel", _SobOutSel ? 1 : 0);
            DeNorSobOutlineMat.SetFloat("_SobelOutlineThreshold", _SobelOutlineThreshold);
            DeNorSobOutlineMat.SetFloat("_WhiThres", (1.0f - _WhiThres));
            DeNorSobOutlineMat.SetFloat("_BlaThres", _BlaThres);

            DeNorSobOutlineMat.SetColor("_OutlineColor", _OutlineColor);
            DeNorSobOutlineMat.SetFloat("_OutlineColorIntensity", _OutlineColorIntensity);
            DeNorSobOutlineMat.SetFloat("_ColOutMiSel", _ColOutMiSel ? 1 : 0);

            DeNorSobOutlineMat.SetFloat("_OutOnSel", _OutOnSel ? 1 : 0);

            DeNorSobOutlineMat.SetFloat("_MixDeNorSob", _MixDeNorSob ? 1 : 0);

            switch (_SobOutSel)
            {
                case true:
                    DeNorSobOutlineMat.EnableKeyword("RENDER_OUTLINE_ALL");
                    break;
                default:
                    DeNorSobOutlineMat.DisableKeyword("RENDER_OUTLINE_ALL");
                    break;
            }

            switch (_MixDeNorSob)
            {
                case true:
                    DeNorSobOutlineMat.EnableKeyword("MIX_DENOR_SOB");
                    break;
                default:
                    DeNorSobOutlineMat.DisableKeyword("MIX_DENOR_SOB");
                    break;
            }

        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {

            CommandBuffer cmd = CommandBufferPool.Get("DeNorSob Outline");

            RenderTextureDescriptor opaqueDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDescriptor.depthBufferBits = 0;

            if (destination == RenderTargetHandle.CameraTarget)
            {
                cmd.GetTemporaryRT(temporaryColorTexture.id, opaqueDescriptor, FilterMode.Point);
                Blit(cmd, source, temporaryColorTexture.Identifier(), DeNorSobOutlineMat, 0);
                Blit(cmd, temporaryColorTexture.Identifier(), source);

            }
            else Blit(cmd, source, destination.Identifier(), DeNorSobOutlineMat, 0);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);

        }

        public override void FrameCleanup(CommandBuffer cmd)
        {

            if (destination == RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(temporaryColorTexture.id);

        }
    }

    [System.Serializable]
    public class DeNorSobOutlineSettings
    {
        [Space(10)]
        [Tooltip("How thick or thin the outline is.")]
        [Min(0)]
        public float OutlineWidth = 1.0f;

        [Space(10)]

        [Header("**Depth and Normal Based Outline**")]
        [Space(20)]
        [Tooltip("This will adjust the depth based outline threshold.")]
        public float DepthThreshold = 900.0f;

        [Space(10)]

        [Tooltip("This will adjust the normal based outline threshold.")]
        public float NormalThreshold = 1.3f;
        [Tooltip("This will adjust the min of the normal to get more normal based outline details.")]
        public float NormalMin = 1.0f;
        [Tooltip("This will adjust the max of the normal to get more normal based outline details.")]
        public float NormalMax = 1.0f;

        [Space(10)]

        [Header("**Sobel Outline**")]
        [Space(20)]
        [Tooltip("This will render outline all on the screen")]
        public bool SobelOutline = false;

        [Tooltip("This will adjust the sobel threshold.\n\n*Sobel Outline is needed to be enabled for this to work.")]
        [Min(0)]
        public float SobelOutlineThreshold = 50.0f;

        [Space(6)]

        [Tooltip("The amount of whites or bright colors to be affected by the outline.\n\n*Sobel Outline is needed to be enabled for this to work.")]
        public float WhiteThreshold = 0.0f;

        [Tooltip("The amount of blacks or dark colors to be affected by the outline.\n\n*Sobel Outline is needed to be enabled for this to work.")]
        public float BlackThreshold = 0.0f;

        [Space(10)]

        [Header("**Color**")]
        [Tooltip("Outline Color")]
        [Space(20)]
        public Color OutlineColor = Color.black;

        [Tooltip("How strong the outline color is.")]
        public float ColorIntensity = 1.0f;

        [Tooltip("Mix full screen color image to the outline color.")]
        public bool MixFullScreenColor = false;

        [Space(10)]

        [Header("**Settings**")]
        [Space(20)]
        [Tooltip("Show the outline only.")]
        public bool ShowOutlineOnly = false;

        [Tooltip("Mix Depth-Normal Based Outline and Sobel Outline.")]
        public bool MixDephNormalAndSobelOutline = false;

        [Space(10)]

        public Shader DeNorSobOutlineShader;
        public RenderPassEvent WhenToInsert = RenderPassEvent.AfterRenderingTransparents;
    }

    public DeNorSobOutlineSettings settings = new DeNorSobOutlineSettings();
    DeNorSobOutlinePass DeNorSobOutlinepass;
    RenderTargetHandle DeNorSobOutlineTex;

    public override void Create()
    {

        this.name = "DeNorSob Outline";
        DeNorSobOutlinepass = new DeNorSobOutlinePass(
          "DeNorSob Outline",
          settings.WhenToInsert,
          settings.DeNorSobOutlineShader = Shader.Find("Hidden/URP/RealToon/Effects/DeNorSobOutline"),
          settings.OutlineWidth,
          settings.DepthThreshold,
          settings.NormalThreshold,
          settings.NormalMin,
          settings.NormalMax,
          settings.SobelOutline,
          settings.SobelOutlineThreshold,
          settings.WhiteThreshold,
          settings.BlackThreshold,
          settings.OutlineColor,
          settings.ColorIntensity,
          settings.MixFullScreenColor,
          settings.ShowOutlineOnly,
          settings.MixDephNormalAndSobelOutline
        );

    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        DeNorSobOutlinepass.Setup(renderer.cameraColorTarget, RenderTargetHandle.CameraTarget);
        renderer.EnqueuePass(DeNorSobOutlinepass);
    }
}


