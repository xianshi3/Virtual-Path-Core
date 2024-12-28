﻿using System;
using System.Reflection;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;

namespace VirtualPathCore.Graphics.OpenGL;

/// <summary>
/// 表示一个OpenGL渲染管线，用于管理着色器程序及其相关操作
/// </summary>
public unsafe class RenderPipeline : GraphicsResource
{
    /// <summary>
    /// 初始化 <see cref="RenderPipeline"/> 类的新实例
    /// </summary>
    /// <param name="graphicsHost">图形宿主对象</param>
    /// <param name="vs">顶点着色器</param>
    /// <param name="fs">片段着色器</param>
    /// <exception cref="Exception">当链接着色器程序失败时抛出</exception>
    public RenderPipeline(IGraphicsHost<GL> graphicsHost, Shader vs, Shader fs) : base(graphicsHost)
    {
        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, vs.Handle);
        GL.AttachShader(Handle, fs.Handle);
        GL.LinkProgram(Handle);

        string error = GL.GetProgramInfoLog(Handle);

        if (!string.IsNullOrEmpty(error))
        {
            GL.DeleteProgram(Handle);

            throw new Exception($"Link: {error}");
        }
    }

    /// <summary>
    /// 销毁渲染管线资源
    /// </summary>
    /// <param name="disposing">指示是否显式释放资源</param>
    protected override void Destroy(bool disposing = false)
    {
        GL.DeleteProgram(Handle);
    }

    /// <summary>
    /// 获取指定属性的位置
    /// </summary>
    /// <param name="name">属性名称</param>
    /// <returns>属性的位置</returns>
    public int GetAttribLocation(string name)
    {
        return GL.GetAttribLocation(Handle, name);
    }

    /// <summary>
    /// 获取指定统一变量的位置
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <returns>统一变量的位置</returns>
    public int GetUniformLocation(string name)
    {
        return GL.GetUniformLocation(Handle, name);
    }

    /// <summary>
    /// 设置整数类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, int value)
    {
        GL.Uniform1(GetUniformLocation(name), value);
    }

    /// <summary>
    /// 设置浮点数类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, float value)
    {
        GL.Uniform1(GetUniformLocation(name), value);
    }

    /// <summary>
    /// 设置二维向量类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, Vector2D<float> value)
    {
        GL.Uniform2(GetUniformLocation(name), value.X, value.Y);
    }

    /// <summary>
    /// 设置三维向量类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, Vector3D<float> value)
    {
        GL.Uniform3(GetUniformLocation(name), value.X, value.Y, value.Z);
    }

    /// <summary>
    /// 设置四维向量类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, Vector4D<float> value)
    {
        GL.Uniform4(GetUniformLocation(name), value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    /// 设置2x2矩阵类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, Matrix2X2<float> value)
    {
        GL.UniformMatrix2(GetUniformLocation(name), 1, false, (float*)&value);
    }

    /// <summary>
    /// 设置3x3矩阵类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, Matrix3X3<float> value)
    {
        GL.UniformMatrix3(GetUniformLocation(name), 1, false, (float*)&value);
    }

    /// <summary>
    /// 设置4x4矩阵类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    public void SetUniform(string name, Matrix4X4<float> value)
    {
        GL.UniformMatrix4(GetUniformLocation(name), 1, false, (float*)&value);
    }

    /// <summary>
    /// 设置结构体类型的统一变量
    /// </summary>
    /// <typeparam name="T">结构体类型</typeparam>
    /// <param name="name">统一变量名称</param>
    /// <param name="value">要设置的值</param>
    /// <exception cref="NotSupportedException">当结构体包含不支持的类型时抛出</exception>
    public void SetUniform<T>(string name, T value) where T : struct
    {
        if (!string.IsNullOrEmpty(name))
        {
            name = $"{name}.";
        }

        foreach (FieldInfo field in value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
            Type fieldType = field.FieldType;

            if (fieldType == typeof(int))
            {
                SetUniform($"{name}{field.Name}", (int)field.GetValue(value)!);
            }
            else if (fieldType == typeof(float))
            {
                SetUniform($"{name}{field.Name}", (float)field.GetValue(value)!);
            }
            else if (fieldType == typeof(Vector2D<float>))
            {
                SetUniform($"{name}{field.Name}", (Vector2D<float>)field.GetValue(value)!);
            }
            else if (fieldType == typeof(Vector3D<float>))
            {
                SetUniform($"{name}{field.Name}", (Vector3D<float>)field.GetValue(value)!);
            }
            else if (fieldType == typeof(Vector4D<float>))
            {
                SetUniform($"{name}{field.Name}", (Vector4D<float>)field.GetValue(value)!);
            }
            else if (fieldType == typeof(Matrix2X2<float>))
            {
                SetUniform($"{name}{field.Name}", (Matrix2X2<float>)field.GetValue(value)!);
            }
            else if (fieldType == typeof(Matrix3X3<float>))
            {
                SetUniform($"{name}{field.Name}", (Matrix3X3<float>)field.GetValue(value)!);
            }
            else if (fieldType == typeof(Matrix4X4<float>))
            {
                SetUniform($"{name}{field.Name}", (Matrix4X4<float>)field.GetValue(value)!);
            }
            else
            {
                throw new NotSupportedException($"不支持的类型：{fieldType}");
            }
        }
    }

    /// <summary>
    /// 设置纹理类型的统一变量
    /// </summary>
    /// <param name="name">统一变量名称</param>
    /// <param name="index">纹理单元索引</param>
    /// <param name="texture">纹理对象</param>
    public void SetUniform(string name, uint index, Texture texture)
    {
        GL.ActiveTexture(GLEnum.Texture0 + (int)index);
        GL.BindTexture(GLEnum.Texture2D, texture.Handle);

        SetUniform(name, (int)index);
    }

    /// <summary>
    /// 绑定渲染管线
    /// </summary>
    public void Bind()
    {
        GL.UseProgram(Handle);

        GL.Enable(GLEnum.DepthTest);
    }

    /// <summary>
    /// 解绑渲染管线
    /// </summary>
    public void Unbind()
    {
        GL.UseProgram(0);
    }
}
