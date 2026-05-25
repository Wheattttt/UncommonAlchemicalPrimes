Uncommon Alchemical Primes adds many extra elements to play around with.

## **THE ORDINALS**  
<img width="454" height="470" alt="image" src="https://github.com/user-attachments/assets/a443af77-86f7-4b91-af5e-e386ae3ac915" />

The Ordinals, pictured above, are special elements that can be calcified and duplicated just like Cardinals, and exist between them.  
The **Glyph of Similarity** will transmute an atom of Salt resting in the bowl, to whichever Ordinal is shared by the two Cardinal Elements in the input bowls.  
The **Glyph of Stability** will transmute two Ordinals into their shared Cardinal, or Salt if the Ordinals are opposed.  
  
While it may seem like they are "Composite" elements of their neighboring cardinal elements, the opposite is true. The Ordinals represent traits shared between their adjacent Cardinals, and are more abstract as a result.  
  
  
## **THE SECOND-ORDER METALS**
<img width="337" height="650" alt="image" src="https://github.com/user-attachments/assets/413950cd-53c3-41c9-886d-824e72ce5d29" />

The Second-Order Metals, pictured above, are a group of five new metals that exist alongside the original metals of antiquity.  
The **Glyph of Osmosis** will take two adjacent metals (For example, Silver and Copper), and convert them to their shared opposite-order metal (two atoms of Cobalt).  
The **Glyph of Dissolution** is a similar transmutation, but in reverse. It will take two of the same metal (For example, two atoms of Iron), and convert them to their opposite order neighbors (Bismuth and Nickel).
Both of these glyphs can take in either base metals, or Second-Order Metals.  
  
It may be useful to categorize them as effectively "half-metals" with metallicity between their neighboring metals, however they actually exist on their own scale of second-order metallicity from 1 to 5. This behavior becomes especially obvious with the Glyph of Deposition.  

## **FLUXISMUS**
<img width="332" height="287" alt="image" src="https://github.com/user-attachments/assets/cda571fb-563c-4757-abea-1d4295b09a4f" />

The Fluxismus atoms are akin to the process of Animismus, but applied to Quicksilver as opposed to Salt.  
The **Glyph of Fluxismus** will take two quicksilver, producing an atom of Muto and an atom of Fixus.

## **CAMPAIGN SUPPORT**
This mod is designed around supported use in campaigns. Any of the above atom groups can be disabled by your campaign by putting the following lines in your campaign's load method:  
``UncommonPrimes.API.OrdinalsEnabled = false;`` will disable the Ordinals, and their glyphs.  
``UncommonPrimes.API.SecondOrderMetalsEnabled = false;`` will disable the Second-Order Metals, and their glyphs.  
``UncommonPrimes.API.FluxismusEnabled = false;`` will disable the Fluxismus Atoms, and their glyphs.  
This also hides said atoms from the periodic table.  

## **DEPENDENCIES**

This mod requires [Quintessential](https://github.com/ErikHaag/Quintessential/releases/tag/v0.5.4) and [Brimstone](https://github.com/ErikHaag/Brimstone/releases/tag/2.0.2).

Additionally, the Second-Order Metals are compatible with [Reductive Metallurgy](https://github.com/icwass/ReductiveMetallurgy/releases/tag/v1.0.1), which is reccomended as well.  
Fluxismus has combatibility with [True Animismus](https://github.com/ItsKazyan/TrueAn/releases/tag/bugfix1.0.5), however it is only partial. Notably, the Disposal Jack cannot be placed on the Fluxismus glyph, and the Glyph of Infusion does not work on Fluxismus atoms.
