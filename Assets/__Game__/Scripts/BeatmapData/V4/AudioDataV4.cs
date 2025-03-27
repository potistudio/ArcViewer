using System;
using System.Collections.Generic;


[Serializable]
public class AudioDataV4 {


	//* Fields *//

	public string            version;
	public string            songChecksum;
	public int               songSampleCount;
	public int               songFrequency;

	public AudioBpmRegion[]  bpmData;
	public AudioLufsRegion[] lufsData;


	//* Constructor *//

	public AudioDataV4() {
		version         = "4.0.0";
		songChecksum    = "";
		songSampleCount = 0;
		songFrequency   = 0;
		bpmData         = new AudioBpmRegion[0];
		lufsData        = new AudioLufsRegion[0];
	}


	//* Logics *//

	public BeatmapBpmEvent[] GetBpmChanges() {
		List<BeatmapBpmEvent> bpmChanges = new List<BeatmapBpmEvent>();

		for (int i = 0; i < bpmData.Length; i++) {
			AudioBpmRegion region = bpmData[i];

			float regionDuration = (float)(region.ei - region.si) / songFrequency;
			float regionBeats = region.eb - region.sb;

			bpmChanges.Add (new BeatmapBpmEvent {
				b = region.sb,
				m = regionBeats / (regionDuration / 60f)
			});
		}

		return bpmChanges.ToArray();
	}
}


//* Structs *//

[Serializable]
public class AudioBpmRegion {
	public int   si;
	public int   ei;
	public float sb;
	public float eb;
}

[Serializable]
public class AudioLufsRegion {
	public int   si;
	public int   ei;
	public float l;
}
