#pragma once

#include "ImageClasse.h"
#include "ImageNdg.h"
#include "ImageCouleur.h"
#include "ImageDouble.h"

#include <windows.h>

class ClibIHM 
{
private:
	int						nbDataImg; // Nb champs image
	std::vector<int>		dataFromImg; // Champs image
	CImageCouleur*          imgPt;       //  Image

public:
	// Constructeurs
	_declspec(dllexport) ClibIHM();

	_declspec(dllexport) ClibIHM(int nbChamps, byte* data, int stride, int nbLig, int nbCol); // Utilisé pour les images

	_declspec(dllexport) ~ClibIHM();

	// Get et set 
	_declspec(dllexport) int lireNbChamps() const 
	{
		return nbDataImg;
	}

	_declspec(dllexport) int lireChamp(int i) const 
	{
		return dataFromImg.at(i);
	}

	_declspec(dllexport) CImageCouleur* imgData() const 
	{
		return imgPt;
	}

	// Methodes liées appli
	_declspec(dllexport) int calibImage();
};

extern "C" _declspec(dllexport) ClibIHM* objetLib()
{
	ClibIHM* pImg = new ClibIHM();
	return pImg;
}

extern "C" _declspec(dllexport) ClibIHM* objetLibDataImg(int nbChamps, byte* data, int stride, int nbLig, int nbCol)
{
	ClibIHM* pImg = new ClibIHM(nbChamps,data,stride,nbLig,nbCol);
	return pImg;
}

extern "C" _declspec(dllexport) int valeurChamp(ClibIHM* pImg, int i)
{
	return pImg->lireChamp(i);
}

extern "C" _declspec(dllexport) int calibImageExtern(ClibIHM* pImg)
{
	return pImg->calibImage();
}