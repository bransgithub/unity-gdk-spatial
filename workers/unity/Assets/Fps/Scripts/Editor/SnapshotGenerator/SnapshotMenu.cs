using System.IO;
using Fps.Config;
using Improbable;
using Improbable.Gdk.Core;
using UnityEditor;
using UnityEngine;

namespace Fps.Editor
{
    public class SnapshotMenu : MonoBehaviour
    {
        private static readonly string DefaultSnapshotPath =
            Path.Combine(Application.dataPath, "../../../snapshots/default.snapshot");

        private static readonly string CloudSnapshotPath =
            Path.Combine(Application.dataPath, "../../../snapshots/cloud.snapshot");

        [MenuItem("SpatialOS/Generate FPS Snapshot")]
        private static void GenerateFpsSnapshot()
        {
            SaveSnapshot(DefaultSnapshotPath, GenerateDefaultSnapshot());
            SaveSnapshot(CloudSnapshotPath, GenerateDefaultSnapshot());
        }

        private static Snapshot GenerateDefaultSnapshot()
        {
            var snapshot = new Snapshot();
            snapshot.AddEntity(FpsEntityTemplates.Spawner(Coordinates.Zero));
            AddHealthPacks(snapshot);
            return snapshot;
        }

        private static void SaveSnapshot(string path, Snapshot snapshot)
        {
            snapshot.WriteToFile(path);
            Debug.LogFormat("Successfully generated initial world snapshot at {0}", path);
        }

        private static void AddHealthPacks(Snapshot snapshot)
        {
            // Invoke our static function to create an entity template of our health pack with 100 health.
            var healthPack = FpsEntityTemplates.HealthPickup(new Vector3(5, 0, 0), 100);

            // Add the entity template to the snapshot.
            snapshot.AddEntity(healthPack);
        }
    }
}