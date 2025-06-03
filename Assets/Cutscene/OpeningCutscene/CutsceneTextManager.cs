using UnityEngine;

public class CutsceneTextManager : MonoBehaviour
{
    public Typewriter typewriter;

    public void Line1() => typewriter.StartTyping("Far beyond six mountains, beyond six forests, and beyond six shimmering lakes — a place whispered of in legends — there was a small, peaceful kingdom. Here, humans, monsters, and creatures of all kinds lived side by side, sharing happiness and harmony in a land untouched by fear.");
    public void Line2() => typewriter.StartTyping("But it wasn’t always this way. Long ago, these different races fought bitterly, each trying to drive the others away. Countless battles stained the land with blood and sorrow. Yet, from this chaos arose a wise and kind king, who united his people and brought lasting peace to the kingdom.");
    public void Line3() => typewriter.StartTyping("The kingdom’s people, absorbed in their daily lives and routines, tried to forget the dark tragedies of the past. They devoted themselves to their duties, believing that their peaceful days would last forever—unaware that shadows were stirring, ready to shatter their calm.");
    public void Line4() => typewriter.StartTyping("One day, deep in a nearby mine, the miners sensed a terrible evil lurking beneath the earth. Slowly, some workers stopped returning from their shifts, and a foul stench of decay began to seep from the dark tunnels. The kingdom braced itself—another war was coming.");


    public void ClearText()
    {
        typewriter.Skip(); // Poka¿ ca³y tekst od razu
    }
}
