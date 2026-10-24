using MonoMod.Utils;
using UncommonPrimes;
using Quintessential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using static Quintessential.Internal.LegacyPuzzleModel;
using static Quintessential.PartCycleDelegate;
using static StringLoader;
using static TextureAssets;

namespace UncommonPrimes;

internal static class PartsCycle
{
    public static void AddAtom(Sim sim, Part part, HexIndex offset, AtomType atomType)
    {
        Molecule molecule = new();
        molecule.AddAtom(new Atom(atomType), part.InFrontBy(offset));
        sim.molecules.Add(molecule);
    }
    public static void RemoveAtom(AtomReference atom) => atom.molecule.RemoveAtom(atom.pos);
    public static void ChangeAtom(AtomReference atom, AtomType newType) => atom.molecule.ReplaceAtom(newType, atom.pos);

    public static bool SimilarityCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe _, bool isCycleStart)
    {
        bool left = sim.GetAtomReference(part, Parts.similarityInput1, new List<Part>(), true).GetOrDefault(out AtomReference atomInputLeft);
        bool right = sim.GetAtomReference(part, Parts.similarityInput2, new List<Part>(), true).GetOrDefault(out AtomReference atomInputRight);
        bool output = sim.FindAtomRelative(part, Parts.similarityOutput).GetOrDefault(out AtomReference atomOutput);
        if (left && right && (output) && (atomOutput.atomType == AtomTypes.salt))
        {
            //check atom type
            foreach (API.SimilarityRecipe recipe in API.SimilarityTransmutation)
            {
                if ((recipe.leftinput == (atomInputLeft.atomType)) && (recipe.rightinput == (atomInputRight.atomType)))
                {
                    ChangeAtom(atomOutput, recipe.output);
                    atomOutput.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, (TransmutationEffectRenderMode)1, atomOutput.atomType, Assets.textures.atoms.projection_effect, 30f);
                    Vector2 centerVec = HexGrid.standardGrid.ToPixelCoords(part.InFrontBy(Parts.similarityOutput));
                    sim.solutionEditor.glyphEffects.Add(new GlyphEffect(sim.solutionEditor, (EffectTimescaleType)1, centerVec, Parts.similarityFlashAnimation, 30f, Vector2.Zero, 0));
                    Sounds.glyph_similarity.PlaySound(sim.solutionEditor.method_506());
                    return true;

                }
            }
        }
        return false;
    }
    public static bool StabilityCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe _, bool isCycleStart)
    {
        bool ordinal1 = sim.FindAtomRelative(part, Parts.similarityOutput).GetOrDefault(out AtomReference OrdinalAtom1);
        bool ordinal2 = sim.FindAtomRelative(part, Parts.similarityOutput).GetOrDefault(out AtomReference OrdinalAtom2);
        if (ordinal1 && ordinal2)
        {
            //check atom type
            foreach (API.StabilityRecipe recipe in API.StabilityTransmutation)
            {
                if ((recipe.ordinalinput1 == (OrdinalAtom1.atomType)) && (recipe.ordinalinput2 == (OrdinalAtom2.atomType)))
                {
                    ChangeAtom(OrdinalAtom1, recipe.output);
                    OrdinalAtom1.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, (TransmutationEffectRenderMode)1, OrdinalAtom1.atomType, Assets.textures.atoms.projection_effect, 30f);
                    ChangeAtom(OrdinalAtom2, recipe.output);
                    OrdinalAtom2.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, (TransmutationEffectRenderMode)1, OrdinalAtom2.atomType, Assets.textures.atoms.projection_effect, 30f);
                    Sounds.glyph_similarity.PlaySound(sim.solutionEditor.method_506());
                    return true;

                }
            }
        }
        return false;
    }
    public static bool OsmosisCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe _, bool isCycleStart)
    {
        if (isCycleStart)
        {
            // Do atoms exist
            if ((sim.FindAtomRelative(part, Parts.osmosisInputLow).GetOrDefault(out AtomReference LowMetal)) && (sim.FindAtomRelative(part, Parts.osmosisInputHigh).GetOrDefault(out AtomReference HighMetal)))
            {
                if ((!sim.FindAtomRelative(part, Parts.osmosisOutput1).HasValue()) && (!sim.FindAtomRelative(part, Parts.osmosisOutput2).HasValue()))
                {
                    // if atom isn't being held or consumed
                    if (!LowMetal.inMultiAtomMolecule && !LowMetal.isHeldByArm && !HighMetal.inMultiAtomMolecule && !HighMetal.isHeldByArm)
                    {

                        //check atom type
                        foreach (API.OsmosisRecipe recipe in API.OsmosisTransmutation)
                        {
                            if (recipe.lowinput == (LowMetal.atomType) && recipe.highinput == (HighMetal.atomType))
                            {
                                RemoveAtom(LowMetal);
                                RemoveAtom(HighMetal);
                                sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { LowMetal }));
                                sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { HighMetal }));
                                simState.isProcessing = true;
                                simState.processingAtoms = new AtomType[1] { recipe.output };
                                Sounds.glyph_osmosis.PlaySound(sim.solutionEditor.method_506());
                                return true;
                            }
                        }
                    }
                }
            }
        }
        else if (simState.isProcessing)
        {
            AddAtom(sim, part, Parts.osmosisOutput1, simState.processingAtoms[0]);
            AddAtom(sim, part, Parts.osmosisOutput2, simState.processingAtoms[0]);
            return true;
        }
        return false;
    }
    public static bool DissolutionCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe _, bool isCycleStart)
    {
        if (isCycleStart)
        {
            // Do atoms exist
            if ((sim.FindAtomRelative(part, Parts.dissolutionInput1).GetOrDefault(out AtomReference InputMetal1)) && (sim.FindAtomRelative(part, Parts.dissolutionInput2).GetOrDefault(out AtomReference InputMetal2)))
            {
                if ((!sim.FindAtomRelative(part, Parts.dissolutionOutputHigh).HasValue()) && (!sim.FindAtomRelative(part, Parts.dissolutionOutputLow).HasValue()))
                {
                    // if atom isn't being held or consumed
                    if (!InputMetal1.inMultiAtomMolecule && !InputMetal1.isHeldByArm && !InputMetal2.inMultiAtomMolecule && !InputMetal2.isHeldByArm)
                    {

                        //check atom type
                        foreach (API.OsmosisRecipe recipe in API.OsmosisTransmutation)
                        {
                            if (recipe.output == (InputMetal1.atomType) && recipe.output == (InputMetal2.atomType))
                            {
                                RemoveAtom(InputMetal1);
                                RemoveAtom(InputMetal2);
                                sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { InputMetal1 }));
                                sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { InputMetal2 }));
                                simState.isProcessing = true;
                                simState.processingAtoms = new AtomType[2] { recipe.lowinput, recipe.highinput };
                                Sounds.glyph_dissolution.PlaySound(sim.solutionEditor.method_506());
                                return true;
                            }
                        }
                    }
                }
            }
        }
        else if (simState.isProcessing)
        {
            AddAtom(sim, part, Parts.dissolutionOutputLow, simState.processingAtoms[0]);
            AddAtom(sim, part, Parts.dissolutionOutputHigh, simState.processingAtoms[1]);
            return true;
        }
        return false;
    }
    public static bool ExchangeCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe _, bool isCycleStart)
    {
        if (isCycleStart)
        {
            // Do atoms exist
            if ((sim.FindAtomRelative(part, Parts.exchangeInputLeft).GetOrDefault(out AtomReference InputMetalLeft)) && (sim.FindAtomRelative(part, Parts.exchangeInputRight).GetOrDefault(out AtomReference InputMetalRight)) && (sim.FindAtomRelative(part, Parts.exchangeBowl).GetOrDefault(out AtomReference InputMetalBowl)))
            {
                if ((!sim.FindAtomRelative(part, Parts.exchangeOutputLeft).HasValue()) && (!sim.FindAtomRelative(part, Parts.exchangeOutputRight).HasValue()))
                {
                    // if atom isn't being held or consumed
                    if (!InputMetalLeft.inMultiAtomMolecule && !InputMetalLeft.isHeldByArm && !InputMetalRight.inMultiAtomMolecule && !InputMetalRight.isHeldByArm)
                    {
                        bool leftValid = false;
                        bool rightValid = false;
                        AtomType leftAtom = AtomTypes.salt; // error handler salt. these should never remain as salt if the glyph fires
                        AtomType rightAtom = AtomTypes.salt;
                        bool bowlValid = false;
                        // check validity
                        foreach (API.HalfDemotionRecipe recipe in API.HalfDemotionTransmutation)
                        {
                            if (recipe.metalinput == (InputMetalLeft.atomType))
                            {
                                leftValid = true;
                                leftAtom = recipe.metaloutput;
                            }
                            if (recipe.metalinput == (InputMetalRight.atomType))
                            {
                                rightValid = true;
                                rightAtom = recipe.metaloutput;
                            }
                        }
                        //if (InputMetalBowl.atomType.successorMetal.HasValue()) // does the bowl metal project into anything?
                        if (AtomTag.AtomTags["om:$successor_proj"].HasEntry(InputMetalBowl.atomType.Id, out Identifier? ProjectionSuccessor))
                        {
                            bowlValid = true;
                        }
                        if (leftValid && rightValid && bowlValid)
                        {
                            ChangeAtom(InputMetalBowl, AtomTypes.GetByID(ProjectionSuccessor.Value));
                            InputMetalBowl.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, (TransmutationEffectRenderMode)1, InputMetalBowl.atomType, Assets.textures.atoms.projection_effect, 30f);
                            RemoveAtom(InputMetalLeft);
                            RemoveAtom(InputMetalRight);
                            sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { InputMetalLeft }));
                            sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { InputMetalRight }));
                            simState.isProcessing = true;
                            simState.processingAtoms = new AtomType[2] { leftAtom, rightAtom };
                            Sounds.glyph_dissolution.PlaySound(sim.solutionEditor.method_506());
                            return true;
                        }
                    }
                }
            }
        }
        else if (simState.isProcessing)
        {
            AddAtom(sim, part, Parts.exchangeOutputLeft, simState.processingAtoms[0]);
            AddAtom(sim, part, Parts.exchangeOutputRight, simState.processingAtoms[1]);
            return true;
        }
        return false;
    }
    public static bool FluxismusCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe _, bool isCycleStart)
    {
        if (isCycleStart)
        {
            // Do atoms exist
            if ((sim.FindAtomRelative(part, Parts.fluxismusInput1).GetOrDefault(out AtomReference Input1)) && (sim.FindAtomRelative(part, Parts.fluxismusInput2).GetOrDefault(out AtomReference Input2)))
            {
                if ((!sim.FindAtomRelative(part, Parts.fluxismusOutputFixus).HasValue()) && (!sim.FindAtomRelative(part, Parts.fluxismusOutputMuto).HasValue()))
                {
                    // if atom isn't being held or consumed
                    if (!Input1.inMultiAtomMolecule && !Input1.isHeldByArm && !Input2.inMultiAtomMolecule && !Input2.isHeldByArm)
                    {

                        //check atom type
                        foreach (API.FluxismusRecipe recipe in API.FluxismusTransmutation)
                        {
                            if (recipe.input == (Input1.atomType) && recipe.input == (Input2.atomType))
                            {
                                RemoveAtom(Input1);
                                RemoveAtom(Input2);
                                sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { Input1 }));
                                sim.solutionEditor.consumptionEffects.Add(new ConsumptionEffect(sim.solutionEditor, new AtomReference[] { Input2 }));
                                simState.isProcessing = true;
                                simState.processingAtoms = new AtomType[2] { recipe.output_lo, recipe.output_hi };
                                Sounds.glyph_dissolution.PlaySound(sim.solutionEditor.method_506());
                                return true;
                            }
                        }
                    }
                }
            }
        }
        else if (simState.isProcessing)
        {
            AddAtom(sim, part, Parts.fluxismusOutputFixus, simState.processingAtoms[0]);
            AddAtom(sim, part, Parts.fluxismusOutputMuto, simState.processingAtoms[1]);
            return true;
        }
        return false;
    }
}