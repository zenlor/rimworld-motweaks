{
  description = "rimworld mods";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-23.11";
    flakelight.url = "github:nix-community/flakelight";
  };

  outputs = { self, flakelight, ... }@inputs:
    flakelight ./. {
      inherit inputs;

      devShell.packages = pkgs: [
        pkgs.libxml2
      ];
    };
}

