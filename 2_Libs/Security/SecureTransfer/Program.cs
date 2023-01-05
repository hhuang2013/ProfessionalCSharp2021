﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<AliceRunner>();
        services.AddTransient<BobRunner>();
    })
    .Build();

var alice = host.Services.GetRequiredService<AliceRunner>();
var bob = host.Services.GetRequiredService<BobRunner>();
var keyAlice = alice.GetPublicKey();
var keyBob = bob.GetPublicKey();
(byte[] iv, byte[] encryptedData) = await alice.GetSecretMessageAsync(keyBob);
await bob.ReadMessageAsync(iv, encryptedData, keyAlice);
