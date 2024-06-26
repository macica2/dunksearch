// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Models/ProtoModels/caption_request_params.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace DunkSearch.Domain.Models.ProtoModels {

  /// <summary>Holder for reflection information generated from Models/ProtoModels/caption_request_params.proto</summary>
  public static partial class CaptionRequestParamsReflection {

    #region Descriptor
    /// <summary>File descriptor for Models/ProtoModels/caption_request_params.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CaptionRequestParamsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ci9Nb2RlbHMvUHJvdG9Nb2RlbHMvY2FwdGlvbl9yZXF1ZXN0X3BhcmFtcy5w",
            "cm90bxIKZHVua3NlYXJjaCI9Cg5DYXB0aW9uUmVxdWVzdBIPCgd2aWRlb0lk",
            "GAEgASgJEhoKEmRlc2lyZWRMYW5ndWFnZVN0chgCIAEoCSJFCg9EZXNpcmVk",
            "TGFuZ3VhZ2USFwoPcHJpbWFyeUxhbmd1YWdlGAEgASgJEhkKEXNlY29uZGFy",
            "eUxhbmd1YWdlGAIgASgJQieqAiREdW5rU2VhcmNoLkRvbWFpbi5Nb2RlbHMu",
            "UHJvdG9Nb2RlbHNiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::DunkSearch.Domain.Models.ProtoModels.CaptionRequest), global::DunkSearch.Domain.Models.ProtoModels.CaptionRequest.Parser, new[]{ "VideoId", "DesiredLanguageStr" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::DunkSearch.Domain.Models.ProtoModels.DesiredLanguage), global::DunkSearch.Domain.Models.ProtoModels.DesiredLanguage.Parser, new[]{ "PrimaryLanguage", "SecondaryLanguage" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CaptionRequest : pb::IMessage<CaptionRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CaptionRequest> _parser = new pb::MessageParser<CaptionRequest>(() => new CaptionRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CaptionRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DunkSearch.Domain.Models.ProtoModels.CaptionRequestParamsReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CaptionRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CaptionRequest(CaptionRequest other) : this() {
      videoId_ = other.videoId_;
      desiredLanguageStr_ = other.desiredLanguageStr_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CaptionRequest Clone() {
      return new CaptionRequest(this);
    }

    /// <summary>Field number for the "videoId" field.</summary>
    public const int VideoIdFieldNumber = 1;
    private string videoId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string VideoId {
      get { return videoId_; }
      set {
        videoId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "desiredLanguageStr" field.</summary>
    public const int DesiredLanguageStrFieldNumber = 2;
    private string desiredLanguageStr_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DesiredLanguageStr {
      get { return desiredLanguageStr_; }
      set {
        desiredLanguageStr_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CaptionRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CaptionRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (VideoId != other.VideoId) return false;
      if (DesiredLanguageStr != other.DesiredLanguageStr) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (VideoId.Length != 0) hash ^= VideoId.GetHashCode();
      if (DesiredLanguageStr.Length != 0) hash ^= DesiredLanguageStr.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (VideoId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(VideoId);
      }
      if (DesiredLanguageStr.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(DesiredLanguageStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (VideoId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(VideoId);
      }
      if (DesiredLanguageStr.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(DesiredLanguageStr);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (VideoId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(VideoId);
      }
      if (DesiredLanguageStr.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(DesiredLanguageStr);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CaptionRequest other) {
      if (other == null) {
        return;
      }
      if (other.VideoId.Length != 0) {
        VideoId = other.VideoId;
      }
      if (other.DesiredLanguageStr.Length != 0) {
        DesiredLanguageStr = other.DesiredLanguageStr;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            VideoId = input.ReadString();
            break;
          }
          case 18: {
            DesiredLanguageStr = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            VideoId = input.ReadString();
            break;
          }
          case 18: {
            DesiredLanguageStr = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class DesiredLanguage : pb::IMessage<DesiredLanguage>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<DesiredLanguage> _parser = new pb::MessageParser<DesiredLanguage>(() => new DesiredLanguage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<DesiredLanguage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DunkSearch.Domain.Models.ProtoModels.CaptionRequestParamsReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public DesiredLanguage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public DesiredLanguage(DesiredLanguage other) : this() {
      primaryLanguage_ = other.primaryLanguage_;
      secondaryLanguage_ = other.secondaryLanguage_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public DesiredLanguage Clone() {
      return new DesiredLanguage(this);
    }

    /// <summary>Field number for the "primaryLanguage" field.</summary>
    public const int PrimaryLanguageFieldNumber = 1;
    private string primaryLanguage_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string PrimaryLanguage {
      get { return primaryLanguage_; }
      set {
        primaryLanguage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "secondaryLanguage" field.</summary>
    public const int SecondaryLanguageFieldNumber = 2;
    private string secondaryLanguage_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SecondaryLanguage {
      get { return secondaryLanguage_; }
      set {
        secondaryLanguage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as DesiredLanguage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(DesiredLanguage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (PrimaryLanguage != other.PrimaryLanguage) return false;
      if (SecondaryLanguage != other.SecondaryLanguage) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (PrimaryLanguage.Length != 0) hash ^= PrimaryLanguage.GetHashCode();
      if (SecondaryLanguage.Length != 0) hash ^= SecondaryLanguage.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (PrimaryLanguage.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(PrimaryLanguage);
      }
      if (SecondaryLanguage.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SecondaryLanguage);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (PrimaryLanguage.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(PrimaryLanguage);
      }
      if (SecondaryLanguage.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SecondaryLanguage);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (PrimaryLanguage.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PrimaryLanguage);
      }
      if (SecondaryLanguage.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SecondaryLanguage);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(DesiredLanguage other) {
      if (other == null) {
        return;
      }
      if (other.PrimaryLanguage.Length != 0) {
        PrimaryLanguage = other.PrimaryLanguage;
      }
      if (other.SecondaryLanguage.Length != 0) {
        SecondaryLanguage = other.SecondaryLanguage;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            PrimaryLanguage = input.ReadString();
            break;
          }
          case 18: {
            SecondaryLanguage = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            PrimaryLanguage = input.ReadString();
            break;
          }
          case 18: {
            SecondaryLanguage = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
