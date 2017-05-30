﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Responses;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("custom")]
    public class CustomController : AuthorizedController
    {
        public CustomController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            RpcType = Enums.RpcRequestType.Custom;
            UpdateAuthorization();
        }

        [System.Web.Http.HttpPost]
        public override HttpResponseMessage Rpc()
        {
            if (!IsAuth)
                throw new Exception("unauthorize access");

            Log.Debug($"HasSignature:{CurrentPlayer.HasSignature}");
            if (CurrentPlayer.HasSignature == false)
            {
                if (ProtoResponse.Unknown6 != null)
                {
                    //POGOProtos.Networking.Envelopes.Signature
                    //connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature
                    var signature = Encryption.Decrypt(
                           ProtoRequest.Unknown6.Unknown2.EncryptedSignature.ToByteArray());
                    var codedStream = new CodedInputStream(signature);
                    var sig = new Signature();
                    sig.MergeFrom(codedStream);
                    if (sig.DeviceInfo != null)
                    {
                        var usrd = CurrentPlayer;
                        usrd.HasSignature = true;
                        usrd.IsIOS = (sig.DeviceInfo.DeviceBrand == "Apple");
                        bool updtrslt = UpdateCurrentPlayer(usrd);
                        if (!updtrslt)
                        {
                            throw new Exception(" CONCURRENT ACCESS ERROR this shouldn't happen");
                        }
                    }
                }
            }

            Log.Debug($"HasSignature:{CurrentPlayer.HasSignature}, Platform:{CurrentPlayer.Platform}");
            Log.Debug($"ProtoRequest.Requests.Count:{ProtoRequest.Requests.Count}");
            if (ProtoRequest.Requests.Count == 0)
            {
                if (ProtoRequest.Unknown6 != null && ProtoRequest.Unknown6.RequestType == 6)
                {
                    Log.Debug($"ProtoRequest.Unknown6.RequestType:{ProtoRequest.Unknown6.RequestType}");
                    return this.EnvelopResponse();
                }
                else
                {
                    throw new Exception("Invalid Request!.");
                }
            }
            RepeatedField<ByteString> requests = this.ProcessRequests();
            return this.EnvelopResponse(requests);
        }
    }
}