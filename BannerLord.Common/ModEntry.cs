using System;
using System.Collections.ObjectModel;
using BannerLord.Common.Xml;
using ReactiveUI;

namespace BannerLord.Common
{

    public sealed class ModEntry : ReactiveObject, IEquatable<ModEntry>
    {
        public bool Equals(ModEntry other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ModEntry other && this == other;
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(this._module);
            hashCode.Add(this._userModData);
            hashCode.Add(this._isChecked);
            hashCode.Add(this._originalSpot);
            hashCode.Add(this._isPointerOver);
            hashCode.Add(this._loadOrderConflicts);
            return hashCode.ToHashCode();
        }

        private Module _module;
        private UserModData _userModData;
        private bool _isChecked;
        private int _originalSpot;
        private bool _isPointerOver;
        private ObservableCollection<LoadOrderConflict> _loadOrderConflicts;

        public bool IsCheckboxEnabled => this._module?.Official == false && this._module?.SingleplayerModule == true;

        public string CheckBoxTooltip => this._module?.Official == true ? "Official module" :
            this._module?.SingleplayerModule != true ? "Not a single player module" : "Nothing to see here, move along";
        public Module Module
        {
            get => this._module;
            set => this.RaiseAndSetIfChanged(ref this._module, value);
        }

        public UserModData UserModData
        {
            get => this._userModData;
            set
            {
                this.RaiseAndSetIfChanged(ref this._userModData, value);
                this._isChecked = this._userModData.IsSelected;
            }
        }

        public ObservableCollection<LoadOrderConflict> LoadOrderConflicts
        {
            get => this._loadOrderConflicts;
            set => this.RaiseAndSetIfChanged(ref this._loadOrderConflicts, value);
        }

        public string DisplayName => this.Module?.Name;

        public bool IsChecked
        {
            get => this._isChecked;
            set
            {
                this.RaiseAndSetIfChanged(ref this._isChecked, value);
                this._userModData.IsSelected = this._isChecked;
            }
        }

        public bool IsPointerOver
        {
            get => this._isPointerOver;
            set => this.RaiseAndSetIfChanged(ref this._isPointerOver, value);
        }

        public int OriginalSpot
        {
            get => this._originalSpot;
            set => this.RaiseAndSetIfChanged(ref this._originalSpot, value);
        }

        public static bool operator ==(ModEntry a, ModEntry b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }

            if (ReferenceEquals(b, null)) return false;

            if (a.UserModData == null || b.UserModData == null) return false;
            if (!string.IsNullOrWhiteSpace(a.Module.Id))
                return a.Module.Id.Equals(b.Module.Id,
                    StringComparison.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(a.Module.Name))
                return a.Module.Name.Equals(b.Module.Name,
                    StringComparison.OrdinalIgnoreCase);

            return false;
        }

        public static bool operator !=(ModEntry a, ModEntry b)
        {
            return !(a == b);
        }
    }
}