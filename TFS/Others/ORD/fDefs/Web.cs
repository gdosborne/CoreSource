//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------
//
// Definitions of classes, structures, enums and interfaces for fWeb namespace
//

using System;
using System.Drawing;
using System.Collections.Generic;
using SNC.OptiRamp.Services.fDefs;

namespace SNC.OptiRamp.Services.fWeb
{
    /// <summary>
    ///     In addition to IDataChannel properties provides Type for web channel
    /// </summary>
    public interface IWebChannel : IDataChannel
    {
        System.Type Type    { get; }  // will return double, bool, System.Drawing.Bitmap
    };

    /// <summary>
    /// 
    /// </summary>
    public enum RuntimePictureStyle
    {
        Black_Background, White_Background
    };

    /// <summary>
    /// 
    /// </summary>
    public enum AxisXDisplayStyle
    {
        GenericValues, TrenderTimeValues
    }

    /// <summary>
    /// 
    /// </summary>
    public class Parameters
    {
        public Rectangle tRect { get; set; }
        public RuntimePictureStyle tRuntimePictureStyle { get; set; }
        public AxisXDisplayStyle tAxisXDisplayStyle { get; set; }
    };

    /// <summary>
    /// IPictureBuilder
    /// </summary>
    public interface IPictureBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pParams"></param>
        void BuildPicture(Graphics g, Parameters pParams, out ResponseStatus rs);
    }; 
}
