// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: POGOProtos/Networking/Requests/Messages/GetInventoryMessage.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Requests.Messages {

  /// <summary>Holder for reflection information generated from POGOProtos/Networking/Requests/Messages/GetInventoryMessage.proto</summary>
  public static partial class GetInventoryMessageReflection {

    #region Descriptor
    /// <summary>File descriptor for POGOProtos/Networking/Requests/Messages/GetInventoryMessage.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GetInventoryMessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CkFQT0dPUHJvdG9zL05ldHdvcmtpbmcvUmVxdWVzdHMvTWVzc2FnZXMvR2V0",
            "SW52ZW50b3J5TWVzc2FnZS5wcm90bxInUE9HT1Byb3Rvcy5OZXR3b3JraW5n",
            "LlJlcXVlc3RzLk1lc3NhZ2VzIkgKE0dldEludmVudG9yeU1lc3NhZ2USGQoR",
            "bGFzdF90aW1lc3RhbXBfbXMYASABKAMSFgoOaXRlbV9iZWVuX3NlZW4YAiAB",
            "KAViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Requests.Messages.GetInventoryMessage), global::POGOProtos.Networking.Requests.Messages.GetInventoryMessage.Parser, new[]{ "LastTimestampMs", "ItemBeenSeen" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GetInventoryMessage : pb::IMessage<GetInventoryMessage> {
    private static readonly pb::MessageParser<GetInventoryMessage> _parser = new pb::MessageParser<GetInventoryMessage>(() => new GetInventoryMessage());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetInventoryMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Networking.Requests.Messages.GetInventoryMessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetInventoryMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetInventoryMessage(GetInventoryMessage other) : this() {
      lastTimestampMs_ = other.lastTimestampMs_;
      itemBeenSeen_ = other.itemBeenSeen_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetInventoryMessage Clone() {
      return new GetInventoryMessage(this);
    }

    /// <summary>Field number for the "last_timestamp_ms" field.</summary>
    public const int LastTimestampMsFieldNumber = 1;
    private long lastTimestampMs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long LastTimestampMs {
      get { return lastTimestampMs_; }
      set {
        lastTimestampMs_ = value;
      }
    }

    /// <summary>Field number for the "item_been_seen" field.</summary>
    public const int ItemBeenSeenFieldNumber = 2;
    private int itemBeenSeen_;
    /// <summary>
    /// TODO: Find out what this is.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ItemBeenSeen {
      get { return itemBeenSeen_; }
      set {
        itemBeenSeen_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GetInventoryMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetInventoryMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (LastTimestampMs != other.LastTimestampMs) return false;
      if (ItemBeenSeen != other.ItemBeenSeen) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (LastTimestampMs != 0L) hash ^= LastTimestampMs.GetHashCode();
      if (ItemBeenSeen != 0) hash ^= ItemBeenSeen.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (LastTimestampMs != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(LastTimestampMs);
      }
      if (ItemBeenSeen != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(ItemBeenSeen);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (LastTimestampMs != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(LastTimestampMs);
      }
      if (ItemBeenSeen != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ItemBeenSeen);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetInventoryMessage other) {
      if (other == null) {
        return;
      }
      if (other.LastTimestampMs != 0L) {
        LastTimestampMs = other.LastTimestampMs;
      }
      if (other.ItemBeenSeen != 0) {
        ItemBeenSeen = other.ItemBeenSeen;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            LastTimestampMs = input.ReadInt64();
            break;
          }
          case 16: {
            ItemBeenSeen = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
